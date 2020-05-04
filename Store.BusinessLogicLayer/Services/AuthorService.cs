using Store.BusinessLogicLayer.Mappers;
using Store.BusinessLogicLayer.Models.Authors;
using Store.BusinessLogicLayer.Models.Base;
using Store.BusinessLogicLayer.Services.Interfaces;
using Store.DataAccessLayer.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using static Store.BusinessLogicLayer.Common.Constants.Error.Constants;

namespace Store.BusinessLogicLayer.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;

        public AuthorService(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        public async Task<BaseModel> CreateAsync(AuthorsModelItem model)
        {
            var response = new BaseModel();
            if (model == null)
            {
                response.Errors.Add(ErrorConstants.ModelIsNull);
                return response;
            }
            var author = AuthorMapper.MapModelToEntity(model);
            var result = await _authorRepository.CreateAsync(author);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToCreateAuthor);
            }
            return response;
        }

        public async Task<BaseModel> UpdateAsync(AuthorsModelItem model)
        {
            var response = new BaseModel();
            if (model == null)
            {
                response.Errors.Add(ErrorConstants.ModelIsNull);
                return response;
            }
            var author = await _authorRepository.GetByIdAsync(model.Id);
            if (author == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindAuthor);
                return response;
            }
            var updatedAuthor = AuthorMapper.MapModelToUpdateEntity(author, model);
            var result = await _authorRepository.UpdateAsync(updatedAuthor);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToUpdateAuthor);
            }
            return response;
        }

        public async Task<BaseModel> DeleteAsync(long id)
        {
            var responce = new BaseModel();
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                responce.Errors.Add(ErrorConstants.ImpossibleToFindAuthor);
                return responce;
            }
            var result = await _authorRepository.DeleteAsync(author);
            if (!result)
            {
                responce.Errors.Add(ErrorConstants.ImpossibleToDeleteAuthor);
            }
            return responce;
        }

        public async Task<AuthorsModelItem> GetByIdAsync(long id)
        {
            var responce = new AuthorsModelItem();
            var author = await _authorRepository.GetByIdAsync(id);
            if (author == null)
            {
                responce.Errors.Add(ErrorConstants.ImpossibleToFindAuthor);
                return responce;
            }
            var authorModel = AuthorMapper.MapEntityToModel(author);
            return authorModel;
        }

        public async Task<AuthorsModel> GetAllAsync(AuthorsFilteringModel model)
        {
            var authorModel = new AuthorsModel();
            var filterModel = FilterMapper.MapAuthorsFilteringModel(model);
            var authors = await _authorRepository.GetFilteredAsync(filterModel);
            if (authors == null || !authors.Data.Any())
            {
                authorModel.Errors.Add(ErrorConstants.ImpossibleToFindAuthor);
            }
            foreach (var author in authors.Data)
            {
                var resultModel = AuthorMapper.MapResponseModelToModelItem(author);
                authorModel.Items.Add(resultModel);
            }
            authorModel.TotalCount = authors.TotalItemsCount;
            return authorModel;
        }

        public async Task<AuthorsModel> GetAuthorsAsync()
        {
            var authorModel = new AuthorsModel();
            var authors = await _authorRepository.GetAllAsync();
            foreach (var author in authors)
            {
                var model = AuthorMapper.MapEntityToModel(author);
                authorModel.Items.Add(model);
            }
            return authorModel;
        }
    }
}
