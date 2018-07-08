using System.Collections.Generic;
using System.Threading.Tasks;

namespace TestTask_Сurrencies.Models
{
    public interface ICurrencyCodeRepository
    {
        Task<IEnumerable<CurrencyCode>> GetCurrencyCodes();
    }
}
