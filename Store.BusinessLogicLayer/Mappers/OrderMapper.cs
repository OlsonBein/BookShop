using Store.BusinessLogicLayer.Models.Cart;
using Store.BusinessLogicLayer.Models.OrderItem;
using Store.BusinessLogicLayer.Models.Orders;
using Store.DataAccessLayer.Models.Response;
using Store.DataAccessLayer.Repositories.EFRepositories;
using System;
using System.Collections.Generic;
using StatusEnum = Store.BusinessLogicLayer.Models.Enums.Entity.Enums;

namespace Store.BusinessLogicLayer.Mappers
{
    public class OrderMapper
    {
        public static OrdersModelItem MapResponseModelToModelItem(OrderResponseModel model)
        {
            var responseModel = new OrdersModelItem
            {
                UserName = model.UserName,
                UserEmail = model.UserEmail,
                Id = model.OrderId,                                
                Description = model.Description,              
                PaymentDate = model.OrderDate.ToShortDateString(),
                OrderStatus = (StatusEnum.StatusType)model.OrderStatus
            };
            decimal amount = 0;
            var orderItemsModel = new List<OrderItemModelItem>();
            foreach (var item in model.OrderItems)
            {
                amount += item.Amount;                
                var orderItem = OrderItemMapper.MapEntityToModel(item);
                orderItemsModel.Add(orderItem);
                responseModel.TotalAmount = amount;
            }            
            responseModel.OrderItems = orderItemsModel;
            return responseModel;
        }

        public static Order MapModelToEntity(OrdersModelItem model)
        {
            var order = new Order 
            {                
                PaymentId = model.PaymentId,
                CreationDate = DateTime.Now,
                UserId = model.UserId ,
                Description = model.Description
            };
            return order;
        }

        public static OrdersModelItem MapEntityToModel(Order order)
        {
            var model = new OrdersModelItem
            {               
                Id = order.Id,
                UserId = order.UserId,
                UserEmail = order.User.Email,
                UserName = $"{order.User.FirstName} {order.User.LastName}",
                PaymentDate = order.CreationDate.ToString(),
                Description = order.Description,
                PaymentId = order.PaymentId
            };
            return model;
        }

        public static Order MapCartModelToEntity(CartModelItem cartModel, long userId, long paymentId)
        {
            var model = new Order()
            {
                UserId = userId,
                PaymentId = paymentId,
                CreationDate = DateTime.Now,
                Description = cartModel.Description
            };           
            return model;
        }
    }
}
