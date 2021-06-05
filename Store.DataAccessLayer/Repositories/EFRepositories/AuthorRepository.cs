using Microsoft.EntityFrameworkCore;
using Store.DataAccessLayer.AppContext;
using Store.DataAccessLayer.Common.Extensions;
using Store.DataAccessLayer.Models.Filter;
using Store.DataAccessLayer.Models.Response;
using Store.DataAccessLayer.Repositories.Base;
using Store.DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccessLayer.Repositories.EFRepositories
{
    public class AuthorRepository : BaseEFRepository<Author>, IAuthorRepository
    {
        private readonly ApplicationContext _dbContext;

        public AuthorRepository(ApplicationContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseModel<List<AuthorsResponseModel>>> GetFilteredAsync(AuthorsFilterModel model)
        {
            var authors = _dbContext.Authors
                 .Where(x => !x.IsRemoved)
                 .AsQueryable();
            if (!string.IsNullOrWhiteSpace(model.SearchString))
            {
                authors = authors.Where(x => x.Name.Contains(model.SearchString));
            }
            var orderedAuthors = authors.AsEnumerable().SortByProperty(model.FilteredColumnType.ToString(), model.SortType);
            var response = orderedAuthors
                .Skip((model.PageCount - 1) * model.PageSize).Take(model.PageSize)
                .AsEnumerable()
                .GroupBy(x => x.Id)
                .Select(group => new AuthorsResponseModel
                { 
                    Author = group.Select(x => x).FirstOrDefault(),
                    PrintingEditionTitles = _dbContext.AuthorInPrintingEditions
                    .Include(x => x.PrintingEdition)
                    .Where(x => !x.IsRemoved)
                    .Where(x => x.AuthorId == group.Key)
                    .Select(x => x.PrintingEdition.Title).ToList()
                }).ToList();
            var count = authors.Count();
            var result = new ResponseModel<List<AuthorsResponseModel>>(response, count);
            return result;
        }
    }   
}
