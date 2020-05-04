using Store.BusinessLogicLayer.Models.Base;
using Store.BusinessLogicLayer.Models.Cart;
using Store.BusinessLogicLayer.Models.Orders;
using System.Threading.Tasks;

namespace Store.BusinessLogicLayer.Services.Interfaces
{
    public interface IOrderService
    {
        Task<BaseModel> DeleteAsync(long id);
        Task<OrdersModel> GetAllAsync(OrdersFilterModel model);
        Task<OrdersModelItem> MakeOrderAsync(CartModelItem model, long userId);
        Task<OrdersModel> GetUserOrdersAsync(OrdersFilterModel model, long id);
    }
}
