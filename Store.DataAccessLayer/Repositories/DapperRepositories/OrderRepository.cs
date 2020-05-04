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
    public class OrderRepository : BaseDapperRepository<Order>, IOrderRepository
    {
        private readonly string _connectionString;
        public OrderRepository()
        {
            _connectionString = DapperConstants.ConnectionString;
        }

        public async Task<ResponseModel<List<Models.Response.OrderResponseModel>>> GetByUserIdAsync(OrdersFilterModel model, long id)
        {                                         
            var sqlQuery = new StringBuilder($@"SELECT userOrder.Id, userOrder.CreationDate, item.ProductType, item.ProductTitle, item.Count, item.Amount
                FROM ( 
                SELECT Orders.Id, Orders.CreationDate
                FROM Orders
                INNER JOIN AspNetUsers ON Orders.UserId = AspNetUsers.Id
                WHERE Orders.IsRemoved = 0 AND Orders.UserId = @id
                ORDER BY Orders.{model.FilteredColumnType} ");
            if (model.SortType == Common.Enums.Filter.Enums.AscendingDescendingSort.Descending)
            {
                sqlQuery.Append("DESC ");
            }
            sqlQuery.Append(@"OFFSET (@pageCount - 1) * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY
                ) AS userOrder 
                INNER JOIN (
                SELECT OrderItems.OrderId, PrintingEditions.Type AS ProductType, PrintingEditions.Title AS ProductTitle,
                OrderItems.Count, OrderItems.Amount
                FROM OrderItems
                INNER JOIN PrintingEditions ON PrintingEditions.Id = OrderItems.PrintingEditionId 
                ) AS item ON userOrder.Id = item.OrderId; SELECT COUNT(Orders.Id) FROM Orders WHERE Orders.UserId = @id ");
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var multi = await connection.QueryMultipleAsync(sqlQuery.ToString(), new
                {
                    id = id,
                    pageCount = model.PageCount,
                    pageSize = model.PageSize
                }))
                {
                    var dapperResponse = await multi.ReadAsync<Models.DapperResponse.OrderResponseModel>();
                    var count = await multi.ReadFirstAsync<int>();
                    var grouppedResponse = dapperResponse.GroupBy(x => x.Id).ToList();
                    var response = CreateResponseModel(grouppedResponse);
                    var result = new ResponseModel<List<Models.Response.OrderResponseModel>>(response, count);
                    return result;
                }
            }
        }

        public async Task<ResponseModel<List<Models.Response.OrderResponseModel>>> GetFilteredAsync(OrdersFilterModel model)
        {
            var sqlQuery = new StringBuilder(@"SELECT userOrder.Id, userOrder.CreationDate, userOrder.Name, userOrder.Email,
                item.ProductType, item.ProductTitle, item.Count, item.Amount 
                FROM ( 
                SELECT Orders.Id, Orders.CreationDate, AspNetUsers.FirstName + ' ' + AspNetUsers.LastName AS Name, AspNetUsers.Email ");
            var orderFilterQuery = new StringBuilder(@"
                FROM Orders INNER JOIN AspNetUsers ON Orders.UserId = AspNetUsers.Id WHERE Orders.IsRemoved = 0 ");
            if (!string.IsNullOrWhiteSpace(model.SearchString))
            {
                orderFilterQuery.Append($@"AND AspNetUsers.FirstName LIKE '%{model.SearchString}%' OR AspNetUsers.LastName LIKE '%{model.SearchString}%'
                    OR AspNetUsers.Email LIKE '%{model.SearchString}%' ");
            }
            sqlQuery.Append(orderFilterQuery);
            sqlQuery.Append($"ORDER BY Orders.{model.FilteredColumnType} ");
            if (model.SortType == Common.Enums.Filter.Enums.AscendingDescendingSort.Descending)
            {
                sqlQuery.Append("DESC ");
            }
            sqlQuery.Append($@"OFFSET (@pageCount - 1) * @pageSize ROWS FETCH NEXT @pageSize ROWS ONLY
                ) AS userOrder INNER JOIN ( 
                SELECT OrderItems.OrderId, PrintingEditions.Type AS ProductType, PrintingEditions.Title AS ProductTitle,
                OrderItems.Count, OrderItems.Amount 
                FROM OrderItems
                INNER JOIN PrintingEditions ON PrintingEditions.Id = OrderItems.PrintingEditionId
                ) AS item ON userOrder.Id = item.OrderId;
                SELECT COUNT(Orders.Id) ");
            sqlQuery.Append(orderFilterQuery);
            using (var connection = new SqlConnection(_connectionString))
            {
                using(var multi = await connection.QueryMultipleAsync(sqlQuery.ToString(), new
                {
                    pageCount = model.PageCount,
                    pageSize = model.PageSize
                }))
                {
                    var dapperResponse = await multi.ReadAsync<Models.DapperResponse.OrderResponseModel>();
                    var count = await multi.ReadFirstAsync<int>();
                    var grouppedResponse = dapperResponse.GroupBy(x => x.Id).ToList();
                    var response = CreateResponseModel(grouppedResponse);
                    var result = new ResponseModel<List<Models.Response.OrderResponseModel>>(response, count);
                    return result;
                }               
            }
        }

        private List<Models.Response.OrderResponseModel> CreateResponseModel(List<IGrouping<long,Models.DapperResponse.OrderResponseModel>> dapperModel)
        {
            var responseModel = dapperModel.Select(group => new OrderResponseModel
            {
                UserName = group.Select(x => x.Name).FirstOrDefault(),
                UserEmail = group.Select(x => x.Email).FirstOrDefault(),
                OrderStatus = group.Select(x => x.Status).FirstOrDefault(),
                OrderId = group.Select(x => x.Id).FirstOrDefault(),
                OrderDate = group.Select(x => x.CreationDate).FirstOrDefault(),
                OrderItems = group.Select(x => new OrderItem
                {
                    OrderId = x.Id,
                    Amount = x.Amount,
                    Count = x.Count,
                    PrintingEdition = new PrintingEdition
                    {
                        Title = x.ProductTitle,
                        Type = x.ProductType,
                        Status = x.Status
                    }
                }).ToList()
            }).ToList();
            return responseModel;           
        }

        public async Task<long> GetOrderIdAsync(long paymentId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                var sqlQuery = $"SELECT Orders.Id FROM Orders WHERE Orders.PaymentId = {paymentId} ";
                var result = await connection.QueryFirstAsync<long>(sqlQuery.ToString());
                return result;
            }
        }
    }
}
