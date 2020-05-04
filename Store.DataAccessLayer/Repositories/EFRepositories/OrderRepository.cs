using Microsoft.EntityFrameworkCore;
using Store.DataAccessLayer.AppContext;
using Store.DataAccessLayer.Common.Extensions;
using Store.DataAccessLayer.Models.Filter;
using Store.DataAccessLayer.Models.Response;
using Store.DataAccessLayer.Repositories.Base;
using Store.DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccessLayer.Repositories.EFRepositories
{
    public class OrderRepository : BaseEFRepositpory<Order>, IOrderRepository
    {
        private readonly ApplicationContext _dbContext;
        public OrderRepository(ApplicationContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        private IQueryable<OrderItem> GetQuery()
        {
            var orderItems = _dbContext.OrderItems
                .Include(x => x.Orders)
                .Include(x => x.PrintingEdition)
                .Include(x => x.Orders.User)
                .Where(x => x.IsRemoved == false)
                .AsQueryable();
            return orderItems;
        }

        private IEnumerable<OrderResponseModel> GetOrders(IEnumerable<OrderItem> query)
        {
            var orders = query               
               .GroupBy(x => x.OrderId)
               .Select(group => new OrderResponseModel
               {   
                   OrderItems = group.Select(x => x).ToList(),                       
                   OrderDate = group.Select(x => x.CreationDate).FirstOrDefault(),
                   OrderId = group.Select(x => x.OrderId).FirstOrDefault(),
                   UserEmail = group.Select(x => x.Orders.User.Email).FirstOrDefault(),
                   UserName = group.Select(x => $"{x.Orders.User.FirstName}  {x.Orders.User.LastName}").FirstOrDefault(),
                   Description = group.Select(x => x.Orders.Description).FirstOrDefault(),
                   Amount = group.Select(x => x.Amount).FirstOrDefault()
               });
            return orders;
        }

        public async Task<ResponseModel<List<OrderResponseModel>>> GetByUserIdAsync(OrdersFilterModel model, long id)
        {
            var orderItems = GetQuery()
                .Where(x => x.Orders.UserId == id);
            var sortedOrders = orderItems.AsEnumerable().SortByProperty(model.FilteredColumnType.ToString(), model.SortType);
            var orders = GetOrders(sortedOrders).ToList();
            var count = orders.Count();
            var resultModel = orders.Skip((model.PageCount - 1) * model.PageSize).Take(model.PageSize).ToList();
            var result = new ResponseModel<List<OrderResponseModel>>(resultModel, count);
            return result;
        }

        public async Task<ResponseModel<List<OrderResponseModel>>> GetFilteredAsync(OrdersFilterModel model)
        {
            var orderItems = GetQuery();
            var substring = model.SearchString.Split(" ");
            if (model.SearchString != null)
            {
                orderItems = orderItems.Where(x => x.Orders.User.Email.Contains(substring[0]) ||
                    x.Orders.User.FirstName.Contains(model.SearchString) ||
                    x.Orders.User.LastName.Contains(substring[0]));
            }
            var sortedOrders = orderItems.AsEnumerable().SortByProperty(typeof(OrderItem).GetProperty(model.FilteredColumnType.ToString()).Name, model.SortType);         
            var orders = GetOrders(sortedOrders).ToList();
            var resultModel = orders.Skip((model.PageCount - 1) * model.PageSize).Take(model.PageSize).ToList();
            var count = orders.Count();
            var result = new ResponseModel<List<OrderResponseModel>>(resultModel, count);
            return result;
        }

        public async Task<long> GetOrderIdAsync(long paymentId)
        {
            var id =  await _dbContext.Orders.Where(x => x.PaymentId == paymentId).Select(x => x.Id).FirstOrDefaultAsync();
            return id;
        }
    }
}
