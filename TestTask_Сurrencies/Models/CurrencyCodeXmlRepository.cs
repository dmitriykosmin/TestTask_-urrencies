using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask_Сurrencies.Models
{
    public class CurrencyCodeXmlRepository : ICurrencyCodeRepository
    {
        private ISerializer<CurrencyCodes> serializer;
        private IDataWebLoader loader;

        private string source = "http://www.cbr.ru/scripts/XML_valFull.asp";

        public CurrencyCodeXmlRepository()
        {
            serializer = new Xml_Serializer<CurrencyCodes>();
            loader = new DataHttpLoader();
        }

        public async Task<IEnumerable<CurrencyCode>> GetCurrencyCodes()
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

            return result.Currency_Codes;
        }
    }
}
