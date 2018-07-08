using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestTask_Сurrencies.Models;

namespace TestTask_Сurrencies.ViewModels
{
    public class SearchResult : CurrencyRate
    {
        public string RateToDollar { get; set; } = "неизвестно";

        public override string Title
        {
            get
            {
                return base.Title +
                    $"\n  {RateToDollar} $  " +
                    $"за 1 единицу";
            }
        }

        public SearchResult(CurrencyRate rate)
        {
            Name = rate.Name;
            Id = rate.Id;
            Nominal = rate.Nominal;
            NumCode = rate.NumCode;
            CharCode = rate.CharCode;
            Value = rate.Value;
        }
    }
}
