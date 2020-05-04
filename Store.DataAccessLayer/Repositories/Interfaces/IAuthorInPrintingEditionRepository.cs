using Store.DataAccessLayer.Models.Filter;
using Store.DataAccessLayer.Models.Response;
using Store.DataAccessLayer.Repositories.EFRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccessLayer.Repositories.Interfaces
{
    public interface IAuthorInPrintingEditionRepository : IBaseEFRepository<AuthorInPrintingEdition>
    {
        Task<ResponseModel<List<PrintingEditionsResponseModel>>> GetFilteredPrintingEditionsAsync(PrintingEditionsFilterModel model);
        Task<List<AuthorInPrintingEdition>> GetAsyncByProductId(long id);
    }
}
