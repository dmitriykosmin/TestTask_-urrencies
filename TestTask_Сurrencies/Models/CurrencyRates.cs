using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestTask_Сurrencies.Models
{
    [Serializable]
    [XmlRoot(ElementName = "ValCurs")]
    public class CurrencyRates
    {
        [XmlAttribute("Date")]
        public string Date { get; set; }

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlElement("Valute")]
        public CurrencyRate[] Currency_Curses;
    }
}
