using Store.BusinessLogicLayer.Models.Authors;
using Store.BusinessLogicLayer.Models.Base;
using System.Threading.Tasks;

namespace Store.BusinessLogicLayer.Services.Interfaces
{
    public interface IAuthorService
    {
        Task<BaseModel> CreateAsync(AuthorsModelItem model);
        Task<BaseModel> UpdateAsync(AuthorsModelItem model);
        Task<BaseModel> DeleteAsync(long id);
        Task<AuthorsModelItem> GetByIdAsync(long id);
        Task<AuthorsModel> GetAllAsync(AuthorsFilteringModel model);
        Task<AuthorsModel> GetAuthorsAsync();
    }
}
