using Store.BusinessLogicLayer.Models.Authors;
using Store.DataAccessLayer.Models.Response;
using Store.DataAccessLayer.Repositories.EFRepositories;
using System;

namespace Store.BusinessLogicLayer.Mappers
{
    public class AuthorMapper
    {
        public static AuthorsModelItem MapEntityToModel(Author author)
        {
            var model = new AuthorsModelItem()
            { 
                Id = author.Id,
                Name = author.Name
            };
            return model;
        }

        public static Author MapModelToEntity(AuthorsModelItem model)
        {
            var author = new Author()
            { 
                CreationDate = DateTime.Now,
                Name = model.Name,
                BooksTitles = model.PrintingEditionTitles
            };
            return author;
        }

        public static AuthorsModelItem MapResponseModelToModelItem(AuthorsResponseModel returnModel)
        {
            var resultModel = new AuthorsModelItem()
            { 
                Id = returnModel.Author.Id,
                Name = returnModel.Author.Name,
                PrintingEditionTitles = returnModel.PrintingEditionTitles
            };
            return resultModel;
        }

        public static Author MapModelToUpdateEntity(Author author, AuthorsModelItem model )
        {
            author.Name = model.Name;
            author.BooksTitles = model.PrintingEditionTitles;
            return author;
        }
    }
}
