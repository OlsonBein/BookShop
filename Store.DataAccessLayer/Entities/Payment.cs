using Store.DataAccessLayer.Repositories.Base;
using Store.DataAccessLayer.Repositories.Interfaces;

namespace Store.DataAccessLayer.Repositories.EFRepositories
{
    public class Payment : BaseEntity
    {
        public string TransactionId { get; set; }
    }
}
