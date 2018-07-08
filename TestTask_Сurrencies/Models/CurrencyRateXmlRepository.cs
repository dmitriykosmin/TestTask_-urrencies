using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestTask_Сurrencies.Models
{
    public class CurrencyRateXmlRepository : ICurrencyRateRepository
    {
        private ISerializer<CurrencyRates> serializer;
        private IDataWebLoader loader;

        private string source = "http://www.cbr.ru/scripts/XML_daily.asp";

        public CurrencyRateXmlRepository()
        {
            serializer = new Xml_Serializer<CurrencyRates>();
            loader = new DataHttpLoader();
        }

        public async Task<IEnumerable<CurrencyRate>> GetCurrencyRates()
        {
            string xml;
            try
            {
                xml = await loader.LoadDataToString(source);
            }
            catch
            {
                return null;
            }

            var result = serializer.DeserializeFromString(xml);

            return result.Currency_Curses;
        }
    }
}
