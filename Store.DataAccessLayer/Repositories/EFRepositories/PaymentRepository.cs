using Microsoft.EntityFrameworkCore;
using Store.DataAccessLayer.AppContext;
using Store.DataAccessLayer.Repositories.Base;
using Store.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccessLayer.Repositories.EFRepositories
{
    public class PaymentRepository : BaseEFRepositpory<Payment>, IPaymentRepository
    {
        private readonly ApplicationContext _dbContext;
        public PaymentRepository(ApplicationContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<long> GetPaymentIdAsync(string transactionId)
        {
            var id = await _dbContext.Payments.Where(x => x.TransactionId == transactionId).Select(x => x.Id).FirstOrDefaultAsync();
            return id;
        }
    }
}
