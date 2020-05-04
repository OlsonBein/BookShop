using Store.BusinessLogicLayer.Models.Base;
using System.Threading.Tasks;

namespace Store.BusinessLogicLayer.Services.Interfaces
{
    public interface IBaseService<T> where T : class
    {
        Task<BaseModel> CreateAsync(T item);
        Task<BaseModel> UpdateAsync(T item);
        Task<BaseModel> DeleteAsync(T item);
        Task<T> GetByIdAsync(long id);
    }
}
