using Microsoft.EntityFrameworkCore;
using Store.DataAccessLayer.AppContext;
using Store.DataAccessLayer.Common.Extensions;
using Store.DataAccessLayer.Models.Filter;
using Store.DataAccessLayer.Models.Response;
using Store.DataAccessLayer.Repositories.Base;
using Store.DataAccessLayer.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Store.DataAccessLayer.Common.Enums.Entity.Enums;

namespace Store.DataAccessLayer.Repositories.EFRepositories
{
    public class AuthorInPrintingEditionRepository : BaseEFRepository<AuthorInPrintingEdition>,  IAuthorInPrintingEditionRepository
    {
        private readonly ApplicationContext _dbContext;

        public AuthorInPrintingEditionRepository(ApplicationContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResponseModel<List<PrintingEditionsResponseModel>>> GetFilteredPrintingEditionsAsync(PrintingEditionsFilterModel model)
        {
            var productTypeEnum = Enum.GetValues(typeof(ProductType)).OfType<ProductType>().ToList();
            var currentProductType = productTypeEnum.Where(x => !model.ProductType.Contains(x));
            var printingEditions = _dbContext.AuthorInPrintingEditions
                .Include(x => x.Author)
                .Include(x => x.PrintingEdition)
                .Where(x => !x.IsRemoved)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(model.SearchString))
            {
                printingEditions = printingEditions.Where(x => x.PrintingEdition.Title.Contains(model.SearchString) ||
                    x.PrintingEdition.Description.Contains(model.SearchString) || x.Author.Name.Contains(model.SearchString));
            }
            if (model.MaxPrice != 0 || model.MinPrice != 0)
            {
                printingEditions = printingEditions.Where(x => x.PrintingEdition.Price >= model.MinPrice &&
                     x.PrintingEdition.Price <= model.MaxPrice);
            }
            foreach (var type in currentProductType)
            {
                printingEditions = printingEditions.Where(x => x.PrintingEdition.Type != type);
            }
            var orderedProducts = printingEditions.AsEnumerable().SortBySubProperty(nameof(PrintingEdition), model.FilteredColumnType.ToString(), model.SortType);
            var products = orderedProducts
                .GroupBy(x => x.PrintingEdition.Id)
                .Select(group =>
                new PrintingEditionsResponseModel
                {
                    Authors = group.Select(x => x.Author).ToList(),
                    PrintingEdition = group.Select(x => x.PrintingEdition).FirstOrDefault()
                }).Skip((model.PageCount - 1) * model.PageSize).Take(model.PageSize).ToList();
            var count = printingEditions.Select(x => x.PrintingEdition.Id).Distinct().Count();
            var result = new ResponseModel<List<PrintingEditionsResponseModel>>(products, count);
            return result;
        }

        public async Task<List<AuthorInPrintingEdition>> GetAsyncByProductId(long id)
        {
            var authorInProduct = await _dbContext.AuthorInPrintingEditions.Where(x => x.PrintingEditionId == id).ToListAsync();
            return authorInProduct;
        }
    }
}
