using Store.BusinessLogicLayer.Helpers.Interfaces;
using System.Collections.Generic;
using static Store.BusinessLogicLayer.Common.Constants.Currency.Constants;
using static Store.BusinessLogicLayer.Models.Enums.Entity.Enums;

namespace Store.BusinessLogicLayer.Helpers
{
    public class CurrencyConverterHelper : ICurrencyConverterHelper
    {
        private readonly Dictionary<Currency, decimal> _currencyBook = new Dictionary<Currency, decimal>()
            {
                {Currency.EUR, CurrencyConstants.EuroConstant },
                { Currency.CHF, CurrencyConstants.SwissFrancConstant },
                { Currency.GBP, CurrencyConstants.PoundConstant},
                { Currency.JPY, CurrencyConstants.JapanenJenConstant},
                { Currency.UAH, CurrencyConstants.HrivnaConstant},
                { Currency.USD, CurrencyConstants.DollarConstant}
            };
        public decimal Convert(Currency fromCurrency, Currency toCurrency, decimal userPrice)
        {           
            var price = userPrice / _currencyBook[fromCurrency] * _currencyBook[toCurrency];
            return price;
        }        
    }
}
