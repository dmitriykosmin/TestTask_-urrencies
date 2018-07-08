using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace TestTask_Сurrencies.Models
{
    [Serializable]
    public class CurrencyCode
    {
        [XmlAttribute("ID")]
        public string Id { get; set; }
        
        public string Name { get; set; }
        
        public string EngName { get; set; }
        
        public int Nominal { get; set; }
        
        public string ParentCode { get; set; }
        
        public string ISO_Num_Code { get; set; }

        public string ISO_Char_Code { get; set; }
    }
}
