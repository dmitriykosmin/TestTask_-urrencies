using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask_Сurrencies.Models
{
    public interface ICurrencyRateRepository
    {
        Task<IEnumerable<CurrencyRate>> GetCurrencyRates();
    }
}
