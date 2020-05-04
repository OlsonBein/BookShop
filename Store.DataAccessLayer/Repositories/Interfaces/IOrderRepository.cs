using Store.DataAccessLayer.Models.Filter;
using Store.DataAccessLayer.Models.Response;
using Store.DataAccessLayer.Repositories.EFRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccessLayer.Repositories.Interfaces
{
    public  interface IOrderRepository : IBaseEFRepository<Order>
    {
        Task<ResponseModel<List<OrderResponseModel>>> GetByUserIdAsync(OrdersFilterModel model, long id);
        Task<ResponseModel<List<OrderResponseModel>>> GetFilteredAsync(OrdersFilterModel model);
        Task<long> GetOrderIdAsync(long paymentId);
    }
}
