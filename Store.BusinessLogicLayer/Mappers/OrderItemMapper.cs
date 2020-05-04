using Store.BusinessLogicLayer.Models.OrderItem;
using Store.DataAccessLayer.Repositories.EFRepositories;
using System;
using BusinessEnum = Store.BusinessLogicLayer.Models.Enums.Entity;

namespace Store.BusinessLogicLayer.Mappers
{
    public class OrderItemMapper
    {
        public static OrderItem MapModelToEntity(OrderItemModelItem model)
        {
            var order = new OrderItem()
            {
                Amount = model.Amount,
                Count = model.Count,
                CreationDate = DateTime.Now,
                OrderId = model.OrderId,
                PrintingEditionId = model.PrintingEditionId             
            };      
            return order;
        }

        public static OrderItemModelItem MapEntityToModel(OrderItem entity)
        {
            var model = new OrderItemModelItem()
            {
                Count = entity.Count,
                ProductTitle = entity.PrintingEdition.Title,
                ProductType = (BusinessEnum.Enums.ProductType)entity.PrintingEdition.Type,
                PrintingEditionId = entity.PrintingEditionId,
                OrderId = entity.OrderId
            };
            return model;
        }
    }
}
