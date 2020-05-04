using Store.DataAccessLayer.AppContext;
using Store.DataAccessLayer.Repositories.Base;
using Store.DataAccessLayer.Repositories.Interfaces;

namespace Store.DataAccessLayer.Repositories.EFRepositories
{
    public class OrderItemRepository : BaseEFRepositpory<OrderItem>, IOrderItemRepository
    {
        private readonly ApplicationContext _dbContext;
        public OrderItemRepository(ApplicationContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
