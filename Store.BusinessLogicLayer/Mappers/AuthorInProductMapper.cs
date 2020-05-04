using Store.DataAccessLayer.Repositories.EFRepositories;
using System;

namespace Store.BusinessLogicLayer.Mappers
{
    public class AuthorInProductMapper
    {
        public static AuthorInPrintingEdition MapToAuthorInProduct(PrintingEdition product, long authorId)
        {
            var authorInProduct = new AuthorInPrintingEdition()
            {
                PrintingEditionId = product.Id,
                CreationDate = DateTime.Now,
                AuthorId = authorId                             
            };
            return authorInProduct;
        }
    }
}
