using static Store.BusinessLogicLayer.Models.Enums.Entity.Enums;

namespace Store.BusinessLogicLayer.Helpers.Interfaces
{
    public interface ICurrencyConverterHelper
    {
       decimal Convert(Currency fromCurrency, Currency toCurrency, decimal userPrice);
    }
}
