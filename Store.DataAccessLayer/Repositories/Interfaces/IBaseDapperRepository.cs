using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DataAccessLayer.Repositories.Interfaces
{
    public interface IBaseDapperRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(long id);
        Task<bool> CreateAsync(T item);
        Task<bool> CreateRangeAsync(List<T> entities);
        Task<bool> UpdateAsync(T item);
        Task<bool> UpdateRangeAsync(List<T> entities);
        Task<bool> DeleteAsync(T item);
        Task<bool> DeleteRangeAsync(List<T> entities);
    }
}
