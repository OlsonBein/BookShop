using Microsoft.EntityFrameworkCore;
using Store.DataAccessLayer.AppContext;
using Store.DataAccessLayer.Models.Response;
using Store.DataAccessLayer.Repositories.Base;
using Store.DataAccessLayer.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace Store.DataAccessLayer.Repositories.EFRepositories
{
    public class PrintingEditionRepository : BaseEFRepositpory<PrintingEdition>, IPrintingEditionRepository
    {
        private readonly ApplicationContext _dbContext;

        public PrintingEditionRepository(ApplicationContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PrintingEditionsResponseModel> GetByProductIdAsync(long productId)
        {
            var authorsInPrintingEditions = _dbContext.AuthorInPrintingEditions
                .Include(x => x.Author)
                .Include(x => x.PrintingEdition)
                .Where(x => !x.IsRemoved)
                .AsQueryable();

            var product = authorsInPrintingEditions
                .Where(x => x.PrintingEditionId == productId)
                .AsEnumerable()
                .GroupBy(x => x.PrintingEditionId).
                Select(group => new PrintingEditionsResponseModel
                {
                    Authors = group.Select(x => x.Author).ToList(),
                    PrintingEdition = group.Select(x => x.PrintingEdition).FirstOrDefault()
                }).FirstOrDefault();
            return product;
        }
    }
}
