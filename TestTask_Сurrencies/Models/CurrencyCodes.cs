using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestTask_Сurrencies.Models
{
    [Serializable]
    [XmlRoot(ElementName = "Valuta")]
    public class CurrencyCodes
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("Item")]
        public CurrencyCode[] Currency_Codes;
    }
}
