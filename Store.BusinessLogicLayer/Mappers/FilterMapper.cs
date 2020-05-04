using Store.BusinessLogicLayer.Models.Authors;
using Store.DataAccessLayer.Models.Filter;
using System.Linq;
using DataEnums = Store.DataAccessLayer.Common.Enums.Filter.Enums;
using DataTypeEnum = Store.DataAccessLayer.Common.Enums.Entity.Enums;


namespace Store.BusinessLogicLayer.Mappers
{
    public class FilterMapper
    {
        public static DataAccessLayer.Models.Filter.UsersFilterModel MapUserFilteringModel(Models.Users.UsersFilterModel model)
        {
            var dalModel = new DataAccessLayer.Models.Filter.UsersFilterModel()
            {
                BlockState = (DataEnums.FilterUserBlock)model.Status,
                SortType = (DataEnums.AscendingDescendingSort)model.SortType,
                FilteredColumnType = (DataEnums.FilterUserByColumn)model.FilteredColumnType,
                PageCount = model.PageCount,
                SearchString = model.SearchString,
                PageSize = model.PageSize
            };
            return dalModel;
        }

        public static AuthorsFilterModel MapAuthorsFilteringModel(AuthorsFilteringModel model)
        {
            var dalModel = new AuthorsFilterModel()
            {
                PageCount = model.PageCount,
                SearchString = model.SearchString,
                PageSize = model.PageSize,
                SortType = (DataEnums.AscendingDescendingSort)model.SortType,
                FilteredColumnType = (DataEnums.FilterAuthorByColumn)model.FilteredColumnType
            };
            return dalModel;
        }

        public static DataAccessLayer.Models.Filter.OrdersFilterModel MapOrdersFilteringModel(Models.Orders.OrdersFilterModel model)
        {
            var dalModel = new DataAccessLayer.Models.Filter.OrdersFilterModel()
            {
                PageCount = model.PageCount,
                SearchString = model.SearchString,
                PageSize = model.PageSize,
                SortType = (DataEnums.AscendingDescendingSort)model.SortType,
                FilteredColumnType = (DataEnums.FilterOrdersByColumns)model.FilteredColumnType
            };
            return dalModel;
        }

        public static DataAccessLayer.Models.Filter.PrintingEditionsFilterModel MapPrintingEditionsFilteringModel(
            Models.PrintingEdition.PrintingEditionsFilterModel model)
        {
            var dalModel = new DataAccessLayer.Models.Filter.PrintingEditionsFilterModel()
            {
                PageCount = model.PageCount,
                PageSize = model.PageSize,
                MinPrice = model.MinPrice,
                MaxPrice = model.MaxPrice,
                ProductType = model.ProductTypes.Cast<DataTypeEnum.ProductType>().ToList(),
                SearchString = model.SearchString,
                FilteredColumnType = (DataEnums.FilterProductByColumns)model.FilteredColumnType,
                SortType = (DataEnums.AscendingDescendingSort)model.SortType
            };
            return dalModel;
        }
    }
}
