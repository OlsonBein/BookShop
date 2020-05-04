using Store.BusinessLogicLayer.Helpers.Interfaces;
using Store.BusinessLogicLayer.Mappers;
using Store.BusinessLogicLayer.Models.Authors;
using Store.BusinessLogicLayer.Models.Base;
using Store.BusinessLogicLayer.Models.PrintingEdition;
using Store.BusinessLogicLayer.Services.Interfaces;
using Store.DataAccessLayer.Repositories.EFRepositories;
using Store.DataAccessLayer.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyEnum = Store.BusinessLogicLayer.Models.Enums.Entity.Enums;
using static Store.BusinessLogicLayer.Common.Constants.Error.Constants;

namespace Store.BusinessLogicLayer.Services
{
    public class PrintingEditionService : IPrintingEditionService
    {
        private readonly IPrintingEditionRepository _printingEditionRepository;
        private readonly IAuthorInPrintingEditionRepository _authorInPrintingEditionsRepository;
        private readonly ICurrencyConverterHelper _currencyConverter;

        public PrintingEditionService(IPrintingEditionRepository printingEditionRepository,
            IAuthorInPrintingEditionRepository authorInBooksRepository,
            ICurrencyConverterHelper currencyConverter)
        {
            _printingEditionRepository = printingEditionRepository;
            _authorInPrintingEditionsRepository = authorInBooksRepository;
            _currencyConverter = currencyConverter;
        }

        public async Task<BaseModel> CreateAsync(PrintingEditionsModelItem model)
        {
            var response = new BaseModel();
            if (model == null)
            {
                response.Errors.Add(ErrorConstants.ModelIsNull);
                return response;
            }
            var changedPrice = _currencyConverter.Convert(model.Currency, CurrencyEnum.Currency.USD, model.Price);
            var product = PrintingEditionMapper.MapModelToEntity(model, changedPrice);
            var result = await _printingEditionRepository.CreateAsync(product);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToCreateProduct);
            }
            response = await CreateAuthorInProductAsync(product, model.Authors);
            return response;
        }

        private async Task<BaseModel> CreateAuthorInProductAsync(PrintingEdition product, ICollection<AuthorsModelItem> authors)
        {
            var response = new BaseModel();
            var authorsInProduct = new List<AuthorInPrintingEdition>();
            foreach (var item in authors)
            {
                var authorInProduct = AuthorInProductMapper.MapToAuthorInProduct(product, item.Id);
                authorsInProduct.Add(authorInProduct);
            }
            var result = await _authorInPrintingEditionsRepository.CreateRangeAsync(authorsInProduct);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToAddAuthorToProduct);
            }
            return response;
        }

        public async Task<BaseModel> UpdateAsync(PrintingEditionsModelItem model)
        {
            var response = new BaseModel();
            if (model == null)
            {
                response.Errors.Add(ErrorConstants.ModelIsNull);
            }
            var existingProduct = await _printingEditionRepository.GetByIdAsync(model.Id);
            if (existingProduct == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindProduct);
                return response;
            }
            var product = PrintingEditionMapper.UpdateEntityWithModel(existingProduct, model);
            var result = await _printingEditionRepository.UpdateAsync(product);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToUpdateProduct);
            }
            response = await UpdateAuthorInProductAsync(product, model.Authors);
            return response;
        }

        private async Task<BaseModel> UpdateAuthorInProductAsync(PrintingEdition product, ICollection<AuthorsModelItem> authors)
        {
            var response = new BaseModel();
            var result = await DeleteAuthorInProductAsync(product);
            if (result.Errors.Any())
            {
                response.Errors.Add(ErrorConstants.ImpossibleToUpdateProduct);
                return response;
            }
            result = await CreateAuthorInProductAsync(product, authors);
            if (result.Errors.Any())
            {
                response.Errors.Add(ErrorConstants.ImpossibleToUpdateProduct);
            }
            return response;
        }

        public async Task<BaseModel> DeleteAsync(long id)
        {
            var response = new BaseModel();
            var product = await _printingEditionRepository.GetByIdAsync(id);
            if (product == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindProduct);
                return response;
            }
            var result = await _printingEditionRepository.DeleteAsync(product);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToDeleteProduct);                
            }
            response = await DeleteAuthorInProductAsync(product);
            return response;
        }

        private async Task<BaseModel> DeleteAuthorInProductAsync(PrintingEdition product)
        {
            var response = new BaseModel();
            var authorInProduct = await _authorInPrintingEditionsRepository.GetAsyncByProductId(product.Id);
            if (authorInProduct == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindProduct);
                return response;
            }
            var result = await _authorInPrintingEditionsRepository.DeleteRangeAsync(authorInProduct);
            if (!result)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToAddAuthorToProduct);
            }
            return response;
        }

        public async Task<PrintingEditionsModel> GetAllAsync(PrintingEditionsFilterModel model)
        {
            var response = new PrintingEditionsModel();
            if (model == null)
            {
                response.Errors.Add(ErrorConstants.ModelIsNull);
            }
            var filterModel = FilterMapper.MapPrintingEditionsFilteringModel(model);
            var printingEditions = await _authorInPrintingEditionsRepository.GetFilteredPrintingEditionsAsync(filterModel);
            if (printingEditions == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindProduct);
                return response;
            }
            var products = new PrintingEditionsModel();
            foreach (var printingEdition in printingEditions.Data)
            {
                var convertedPrice = _currencyConverter.Convert(CurrencyEnum.Currency.USD, model.Currency, printingEdition.PrintingEdition.Price);
                var printingEditionItemModel = PrintingEditionMapper.MapProductResponseModelToModelItem(printingEdition, model.Currency, convertedPrice);            
                products.Items.Add(printingEditionItemModel);
            }
            products.TotalCount = printingEditions.TotalItemsCount;
            return products;
        }

        public async Task<PrintingEditionsModelItem> GetById(long id)
        {
            var response = new PrintingEditionsModelItem();
            if (id == 0)
            {
                response.Errors.Add(ErrorConstants.ModelIsNull);
                return response;
            }
            var product = await _printingEditionRepository.GetByProductIdAsync(id);
            if (product == null)
            {
                response.Errors.Add(ErrorConstants.ImpossibleToFindProduct);
                return response;
            }
            var productModel = PrintingEditionMapper.MapEntityToModel(product.PrintingEdition);
            foreach(var item in product.Authors)
            {
                var author = AuthorMapper.MapEntityToModel(item);
                productModel.Authors.Add(author);                
            }
            return productModel;
        }
    }
}
