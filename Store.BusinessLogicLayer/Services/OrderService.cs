using Store.BusinessLogicLayer.Mappers;
using Store.BusinessLogicLayer.Models.Base;
using Store.BusinessLogicLayer.Models.Cart;
using Store.BusinessLogicLayer.Models.OrderItem;
using Store.BusinessLogicLayer.Models.Orders;
using Store.BusinessLogicLayer.Services.Interfaces;
using Store.DataAccessLayer.Repositories.EFRepositories;
using Store.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Store.BusinessLogicLayer.Common.Constants.Error.Constants;

namespace Store.BusinessLogicLayer.Services
{
    public class OrderService : IOrderService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository, IPaymentRepository paymentRepository, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _paymentRepository = paymentRepository;
            _orderItemRepository = orderItemRepository;
        }

        private async Task<BaseModel> CreateAsync(CartModelItem model, long userId)
        {
            var paymentId = await GetPaymentIdAsync(model.TransactionId);
            var order = OrderMapper.MapCartModelToEntity(model, userId, paymentId);
            var response = new OrdersModel();
            if (model == null)
            {
                response.Errors.Add(ErrorConstants.ModelIsNull);
                return response;
            }
            var result = await _orderRepository.CreateAsync(order);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToCreateOrder);
            }
            return response;
        }

        private async Task<BaseModel> CreatePaymentAsync(string transactionId)
        {
            var response = new BaseModel();
            var paymentId = await _paymentRepository.GetPaymentIdAsync(transactionId);
            if (paymentId != 0)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToCreatePayment);
                return response;
            }
            var payment = new Payment()
            {
                TransactionId = transactionId,
                CreationDate = DateTime.Now
            };
            var result = await _paymentRepository.CreateAsync(payment);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToCreatePayment);
            }
            return response;
        }

        private async Task<OrderItemModelItem> CreateOrderItemsAsync(CartModelItem model)
        {
            var paymentId = await GetPaymentIdAsync(model.TransactionId);
            var orderId = await GetOrderIdAsync(paymentId);
            var response = new OrderItemModelItem();
            var items = new List<OrderItem>();
            foreach (var item in model.OrderItems)
            {
                var orderItem = OrderItemMapper.MapModelToEntity(item);
                orderItem.OrderId = orderId;
                items.Add(orderItem);
            }
            var result = await _orderItemRepository.CreateRangeAsync(items);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToCreateOrderItem);
            }
            response.OrderId = orderId;
            return response;
        }

        private async Task<long> GetOrderIdAsync(long paymentId)
        {
            var id = await _orderRepository.GetOrderIdAsync(paymentId);
            return id;
        }

        private async Task<long> GetPaymentIdAsync(string transactionId)
        {
            var id = await _paymentRepository.GetPaymentIdAsync(transactionId);
            return id;
        }

        public async Task<OrdersModelItem> MakeOrderAsync(CartModelItem model, long userId)
        {
            var response = new OrdersModelItem();
            var result = await CreatePaymentAsync(model.TransactionId);
            if (result.Errors.Any())
            {
                return (OrdersModelItem)result;
            }
            result = await CreateAsync(model, userId);
            if (result.Errors.Any())
            {
                return (OrdersModelItem)result;
            }
            var createItemResult = await CreateOrderItemsAsync(model);
            if (result.Errors.Any())
            {
                return (OrdersModelItem)result;
            }
            response.Id = createItemResult.OrderId;
            return response;
        }

        public async Task<OrdersModel> GetAllAsync(OrdersFilterModel model)
        {
            var response = new OrdersModel();
            if (model == null)
            {
                response.Errors.Add(ErrorConstants.ModelIsNull);
                return response;
            }
            var filterModel = FilterMapper.MapOrdersFilteringModel(model);
            var orders = await _orderRepository.GetFilteredAsync(filterModel);
            if (orders == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindOrder);
                return response;
            }
            var responceOrders = new OrdersModel();
            foreach (var order in orders.Data)
            {
                var orderItemModel = OrderMapper.MapResponseModelToModelItem(order);
                responceOrders.Items.Add(orderItemModel);
            }
            responceOrders.TotalCount = orders.TotalItemsCount;
            return responceOrders;
        }

        public async Task<BaseModel> DeleteAsync(long id)
        {
            var response = new BaseModel();
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindOrder);
                return response;
            }
            var result = await _orderRepository.DeleteAsync(order);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToDeleteOrder);
            }
            return response;
        }

        public async Task<OrdersModel> GetUserOrdersAsync(OrdersFilterModel model, long id)
        {
            var response = new OrdersModel();
            if (id == 0)
            {
                response.Errors.Add(ErrorConstants.ModelIsNull);
                return response;
            }
            var filterModel = FilterMapper.MapOrdersFilteringModel(model);
            var orders = await _orderRepository.GetByUserIdAsync(filterModel, id);
            if (orders == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindOrder);
                return response;
            }
            var responceOrders = new OrdersModel();
            foreach (var order in orders.Data)
            {
                var orderItemModel = OrderMapper.MapResponseModelToModelItem(order);
                responceOrders.Items.Add(orderItemModel);
            }
            responceOrders.TotalCount = orders.TotalItemsCount;
            return responceOrders;
        }
    }
}
