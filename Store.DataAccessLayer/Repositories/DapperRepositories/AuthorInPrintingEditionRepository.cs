using Dapper;
using Microsoft.Data.SqlClient;
using Store.DataAccessLayer.Models.Filter;
using Store.DataAccessLayer.Models.Response;
using Store.DataAccessLayer.Repositories.Base;
using Store.DataAccessLayer.Repositories.EFRepositories;
using Store.DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Store.DataAccessLayer.Common.Constants.Constants;

namespace Store.DataAccessLayer.Repositories.DapperRepositories
{
    public class AuthorInPrintingEditionRepository: BaseDapperRepository<AuthorInPrintingEdition>, IAuthorInPrintingEditionRepository
    {
        private readonly string _connectionString;
        public AuthorInPrintingEditionRepository()
        {
            _connectionString = DapperConstants.ConnectionString;
        }

        public async Task<ResponseModel<List<PrintingEditionsResponseModel>>> GetFilteredPrintingEditionsAsync(PrintingEditionsFilterModel model)
        {
            var productTypes = model.ProductType.Select(x => x.GetHashCode()).ToArray();
            var sqlQuery = new StringBuilder($@"SELECT DISTINCT printingEdition.Id, printingEdition.Title, printingEdition.Type, printingEdition.Description,
                printingEdition.Price, author.AuthorName FROM (
	            SELECT DISTINCT PrintingEditions.Id, PrintingEditions.Description, PrintingEditions.Price, PrintingEditions.Title, PrintingEditions.Type ");
            var productFilterQuery = new StringBuilder(@"
	            FROM PrintingEditions
	            LEFT JOIN AuthorInPrintingEditions ON PrintingEditionId = PrintingEditions.Id
	            INNER JOIN Authors ON AuthorId = Authors.Id
	            WHERE PrintingEditions.IsRemoved = 0 AND AuthorInPrintingEditions.IsRemoved = 0 ");
            if (!string.IsNullOrWhiteSpace(model.SearchString))
            {
                productFilterQuery.Append($"AND (PrintingEditions.Title LIKE '%{model.SearchString}%' OR Authors.Name LIKE '%{model.SearchString}%') ");
            }
            productFilterQuery.Append("AND PrintingEditions.Type in (");
            foreach (var item in productTypes)
            {
                productFilterQuery.Append($"{item},");
            }
            productFilterQuery.Remove(productFilterQuery.Length - 1, 1);
            productFilterQuery.Append(") ");
            if (model.MinPrice != 0 || model.MaxPrice != 0)
            {
                productFilterQuery.Append("AND PrintingEditions.Price BETWEEN @minPrice AND @maxPrice ");
            }
            sqlQuery.Append(productFilterQuery);
            sqlQuery.Append($@"ORDER BY PrintingEditions.{model.FilteredColumnType} ");
            if (model.SortType == Common.Enums.Filter.Enums.AscendingDescendingSort.Descending)
            {
                sqlQuery.Append("DESC ");
            }
            sqlQuery.Append(@"OFFSET (@pageCount -1) * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY ) AS printingEdition
                INNER JOIN (
	            SELECT Authors.Name AS AuthorName, PrintingEditionId
	            FROM Authors
	            INNER JOIN AuthorInPrintingEditions ON AuthorId = Authors.Id 
	            WHERE AuthorInPrintingEditions.IsRemoved = 0
                ) AS author ON printingEdition.Id = PrintingEditionId ");
            sqlQuery.Append($@"ORDER BY printingEdition.{model.FilteredColumnType} ");
            if (model.SortType == Common.Enums.Filter.Enums.AscendingDescendingSort.Descending)
            {
                sqlQuery.Append("DESC ");
            }
            sqlQuery.Append("; ");
            sqlQuery.Append("SELECT COUNT(DISTINCT PrintingEditions.Id) ");
            sqlQuery.Append(productFilterQuery);
            var q = sqlQuery.ToString();
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var multi = await connection.QueryMultipleAsync(sqlQuery.ToString(), new
                {
                    minPrice = model.MinPrice,
                    maxPrice = model.MaxPrice,
                    pageCount = model.PageCount,
                    pageSize = model.PageSize
                }))
                {
                    var dapperResponse = await multi.ReadAsync<Models.DapperResponse.PrintingEditionsResponseModel>();
                    var count = await multi.ReadFirstAsync<int>();
                    var grouppingModel = dapperResponse.GroupBy(x => x.Id).ToList();
                    var responseModel = CreateResponseModel(grouppingModel);
                    var result = new ResponseModel<List<PrintingEditionsResponseModel>>(responseModel, count);
                    return result;
                }
            }
        }
        public async Task<List<AuthorInPrintingEdition>> GetAsyncByProductId(long id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"SELECT AuthorInPrintingEditions.Id, AuthorId, PrintingEditionId, CreationDate FROM AuthorInPrintingEditions WHERE PrintingEditionId = {id} ";
                var result = await connection.QueryAsync<AuthorInPrintingEdition>(sqlQuery.ToString());
                return result.ToList();
            }
        }

        private List<PrintingEditionsResponseModel> CreateResponseModel(List<IGrouping<long, Models.DapperResponse.PrintingEditionsResponseModel>> dapperModel)
        {
            var responseModel = dapperModel.Select(group => new PrintingEditionsResponseModel
            {
                PrintingEdition = new PrintingEdition
                {
                    Description = group.Select(x => x.Description).FirstOrDefault(),
                    Id = group.Select(x => x.Id).FirstOrDefault(),
                    Price = group.Select(x => x.Price).FirstOrDefault(),
                    Title = group.Select(x => x.Title).FirstOrDefault(),
                    Type = group.Select(x => x.Type).FirstOrDefault()
                },
                Authors = group.Select(x => new Author
                {
                    Name = x.AuthorName
                }).ToList()
            }).ToList();
            return responseModel;
        }
    }
}

