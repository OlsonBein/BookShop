using Dapper;
using Microsoft.Data.SqlClient;
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
    public class PrintingEditionRepository: BaseDapperRepository<PrintingEdition>, IPrintingEditionRepository
    {
        private readonly string _connectionString;
        public PrintingEditionRepository()
        {
            _connectionString = DapperConstants.ConnectionString;
        }
        public async Task<PrintingEditionsResponseModel> GetByProductIdAsync(long productId)
        {
            var sqlQuery = new StringBuilder(@"SELECT product.Id, product.Description, product.Price, product.Title, author.AuthorName FROM (
	                SELECT PrintingEditions.Id, PrintingEditions.Description, PrintingEditions.Title, PrintingEditions.Price
	                FROM PrintingEditions
	                WHERE PrintingEditions.Id = @productId
                ) AS product
                INNER JOIN (
	                SELECT Authors.Name AS AuthorName, PrintingEditionId
	                FROM Authors
	                INNER JOIN AuthorInPrintingEditions ON AuthorId = Authors.Id
                ) as author ON PrintingEditionId = product.Id"); 
            using (var connection = new SqlConnection(_connectionString))
            {
                var product = await connection.QueryAsync<Models.DapperResponse.PrintingEditionsResponseModel>(sqlQuery.ToString(), new 
                { 
                    productId = productId
                });
                var queryableProduct = product.GroupBy(x => x.Id).ToList();
                var responseModel = CreateResponseModel(queryableProduct);
                return responseModel;
            }
        }

        private PrintingEditionsResponseModel CreateResponseModel(List<IGrouping<long, Models.DapperResponse.PrintingEditionsResponseModel>> dapperModel)
        {
            var responseModel = dapperModel.Select(group => new PrintingEditionsResponseModel
            { 
                PrintingEdition = new PrintingEdition
                {
                    Id = group.Select(x => x.Id).FirstOrDefault(),
                    Description = group.Select(x => x.Description).FirstOrDefault(),
                    Title = group.Select(x => x.Title).FirstOrDefault(),
                    Price = group.Select(x => x.Price).FirstOrDefault()
                },
                Authors = group.Select(x => new Author
                {
                    Name = x.AuthorName
                }).ToList()
            }).FirstOrDefault();
            return responseModel;
        }
    }
}
