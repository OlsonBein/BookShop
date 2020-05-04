using Store.DataAccessLayer.Models.Response;
using Store.DataAccessLayer.Repositories.EFRepositories;
using System.Threading.Tasks;

namespace Store.DataAccessLayer.Repositories.Interfaces
{
    public interface IPrintingEditionRepository : IBaseEFRepository<PrintingEdition>
    {
        Task<PrintingEditionsResponseModel> GetByProductIdAsync(long productId);
    }
}
    