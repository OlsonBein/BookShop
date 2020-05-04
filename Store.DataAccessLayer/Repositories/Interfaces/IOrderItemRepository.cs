using Store.DataAccessLayer.Repositories.EFRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccessLayer.Repositories.Interfaces
{
    public interface IOrderItemRepository : IBaseEFRepository<OrderItem>
    {
    }
}
