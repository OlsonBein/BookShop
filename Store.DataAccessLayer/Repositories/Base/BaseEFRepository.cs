using Microsoft.EntityFrameworkCore;
using Store.DataAccessLayer.AppContext;
using Store.DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccessLayer.Repositories.Base
{
    public class BaseEFRepositpory<TEntity> : IBaseEFRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationContext _dbContext;

        public BaseEFRepositpory(ApplicationContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            var responseEntities = await _dbContext.Set<TEntity>().AsNoTracking()
                .Where(x => !x.IsRemoved).ToListAsync();
            return responseEntities;
        }

        public async Task<bool> CreateAsync(TEntity entity)
        {
            await _dbContext.Set<TEntity>().AddAsync(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<bool> CreateRangeAsync(List<TEntity> entities)
        {
            await _dbContext.Set<TEntity>().AddRangeAsync(entities);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<TEntity> GetByIdAsync(long id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);            
        }

        public async Task<bool> DeleteAsync(TEntity entity)
        {
            entity.IsRemoved = true;
            _dbContext.Set<TEntity>().Update(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<bool> DeleteRangeAsync(List<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsRemoved = true;
            }
            _dbContext.Set<TEntity>().UpdateRange(entities);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<bool> UpdateAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<bool> UpdateRangeAsync(List<TEntity> entities)
        {
            _dbContext.Set<TEntity>().UpdateRange(entities);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }
    }
}
