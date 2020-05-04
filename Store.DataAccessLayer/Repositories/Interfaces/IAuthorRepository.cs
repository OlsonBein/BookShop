using Store.DataAccessLayer.Models.DapperResponse;
using Store.DataAccessLayer.Models.Filter;
using Store.DataAccessLayer.Models.Response;
using Store.DataAccessLayer.Repositories.EFRepositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccessLayer.Repositories.Interfaces
{
    public interface IAuthorRepository : /*IBaseEFRepository<Author>,*/ IBaseDapperRepository<Author>
    {
        Task<ResponseModel<List<AuthorsResponseModel>>> GetFilteredAsync(AuthorsFilterModel model);
    }
}
