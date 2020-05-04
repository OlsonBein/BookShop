using Store.DataAccessLayer.Repositories.EFRepositories;
using System.Threading.Tasks;

namespace Store.DataAccessLayer.Repositories.Interfaces
{
    public interface IPaymentRepository : IBaseEFRepository<Payment>
    {
        Task<long> GetPaymentIdAsync(string transactionId);
    }
}
