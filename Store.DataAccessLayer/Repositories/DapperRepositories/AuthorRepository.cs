using Dapper;
using Microsoft.Data.SqlClient;
using Store.DataAccessLayer.Models.DapperResponse;
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
    public class AuthorRepository : BaseDapperRepository<Author>, IAuthorRepository
    {
        private readonly string _connectionString;
        public AuthorRepository()
        {
            _connectionString = DapperConstants.ConnectionString;
        }

        public async Task<ResponseModel<List<AuthorsResponseModel>>> GetFilteredAsync(AuthorsFilterModel model)
        {
            var sqlQuery = new StringBuilder(@"SELECT author.Id, author.Name, product.PrintingEditionTitle FROM (
	            SELECT AUthors.Id, Authors.Name ");
            var authorFilterQuery = new StringBuilder(@"
	            FROM Authors
	            WHERE Authors.IsRemoved = 0 ");
            if (!string.IsNullOrWhiteSpace(model.SearchString))
            {
                authorFilterQuery.Append($"AND Authors.Name LIKE '%{model.SearchString}%' ");
            }
            sqlQuery.Append(authorFilterQuery);
            sqlQuery.Append($"ORDER BY Authors.{model.FilteredColumnType} ");
            if (model.SortType == Common.Enums.Filter.Enums.AscendingDescendingSort.Descending)
            {
                sqlQuery.Append("DESC ");
            }
            sqlQuery.Append($@"OFFSET (@pageCount -1) * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY
                )AS author
                LEFT JOIN (
	                SELECT PrintingEditions.Title AS PrintingEditionTitle, AuthorId
	                FROM PrintingEditions
	                INNER JOIN AuthorInPrintingEditions ON PrintingEditionId = PrintingEditions.Id
	                WHERE PrintingEditions.IsRemoved = 0 AND AuthorInPrintingEditions.IsRemoved = 0
                ) AS product ON author.Id = AuthorId;
                SELECT COUNT(Authors.Id) ");
            sqlQuery.Append(authorFilterQuery);
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var multi = await connection.QueryMultipleAsync(sqlQuery.ToString(), new
                {
                    pageCount = model.PageCount,
                    pageSize = model.PageSize
                }))
                {
                    var authors = await multi.ReadAsync<AuthorsDapperModel>();
                    var count = await multi.ReadFirstAsync<int>();
                    var grouppedAuthors = authors.GroupBy(x => x.Id).ToList();
                    var response = CreateResponseModel(grouppedAuthors);
                    var result = new ResponseModel<List<AuthorsResponseModel>>(response, count);
                    return result;
                }                                   
            }                       
        }

        private List<AuthorsResponseModel> CreateResponseModel(List<IGrouping<long, AuthorsDapperModel>> dapperModel)
        {
            var authorsResponseModels = dapperModel.Select(group => new AuthorsResponseModel
            {
                Author = new Author
                {
                    Id = group.Select(x => x.Id).FirstOrDefault(),
                    Name = group.Select(x => x.Name).FirstOrDefault()
                },
                PrintingEditionTitles = group.Select(x => x.PrintingEditionTitle).ToList()
            }).ToList();         
            return authorsResponseModels;
        }      
    }
}
