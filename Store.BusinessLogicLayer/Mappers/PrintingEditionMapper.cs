using Store.BusinessLogicLayer.Models.PrintingEdition;
using Store.DataAccessLayer.Models.Response;
using Store.DataAccessLayer.Repositories.EFRepositories;
using BusinessEnums = Store.BusinessLogicLayer.Models.Enums.Entity.Enums;
using DataEnums = Store.DataAccessLayer.Common.Enums.Entity.Enums;
using System;
using Store.BusinessLogicLayer.Models.Authors;
using System.Collections.Generic;
using static Store.BusinessLogicLayer.Models.Enums.Entity.Enums;

namespace Store.BusinessLogicLayer.Mappers
{
    public class PrintingEditionMapper
    {      
        public static PrintingEditionsModelItem MapProductResponseModelToModelItem(
            PrintingEditionsResponseModel model,
            Currency currency,
            decimal convertedPrice)
        {
            var itemModel = new PrintingEditionsModelItem()
            { 
                ProductType = (BusinessEnums.ProductType)model.PrintingEdition.Type,
                Title = model.PrintingEdition.Title,
                Currency = currency,
                Description = model.PrintingEdition.Description,
                Id = model.PrintingEdition.Id,
                Price = convertedPrice,
                Sale = model.PrintingEdition.Sale,
                Status = (BusinessEnums.StatusType)model.PrintingEdition.Status
            };
            var authorModel = new List<AuthorsModelItem>();
            foreach (var item in model.Authors)
            {
                var author = AuthorMapper.MapEntityToModel(item);
                authorModel.Add(author);
            }
            itemModel.Authors = authorModel;
            return itemModel;
        }

        public static PrintingEdition MapModelToEntity(PrintingEditionsModelItem model, decimal changedPrice)
        {
            var result = new PrintingEdition()
            {
                CreationDate = DateTime.Now,
                Currency = DataEnums.Currency.USD,
                Description = model.Description,
                Id = model.Id,
                Price = changedPrice,
                Status = (DataEnums.StatusType)model.Status,
                Title = model.Title,
                Type = (DataEnums.ProductType)model.ProductType,
                Sale = model.Sale
            };
            return result;
        }

        public static PrintingEditionsModelItem MapEntityToModel(PrintingEdition edition)
        {
            var result = new PrintingEditionsModelItem()
            {
                Currency = (BusinessEnums.Currency)edition.Currency,
                Description = edition.Description,
                Id = edition.Id,
                Price = edition.Price,
                Status = (BusinessEnums.StatusType)edition.Status,
                Title = edition.Title,
                ProductType = (BusinessEnums.ProductType)edition.Type,
                Sale = edition.Sale
            };
            return result;
        }

        public static PrintingEdition UpdateEntityWithModel(PrintingEdition edition, PrintingEditionsModelItem model)
        {
            edition.Type = (DataEnums.ProductType)model.ProductType;
            edition.Title = model.Title;
            edition.Status = (DataEnums.StatusType)model.Status;
            edition.Price = model.Price;
            edition.Description = model.Description;
            edition.Currency = (DataEnums.Currency)model.Currency;
            edition.Sale = model.Sale;
            return edition;            
        }
    }
}
