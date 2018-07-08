using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestTask_Сurrencies.Models
{
    [Serializable]
    public class CurrencyRate
    {
        [XmlAttribute("ID")]
        public string Id { get; set; }

        public string Name { get; set; }

        public int Nominal { get; set; }

        public string NumCode { get; set; }

        public string CharCode { get; set; }

        public string Value { get; set; }

        public virtual string Title
        {
            get
            {
                return $"{Name}  {Value} ₽  " +
                    $"за {Nominal} " +
                    $"{(Nominal == 1 ? "единицу" : Nominal < 5 ? "единицы" : "единиц")}";
            }
        }
    }
}
