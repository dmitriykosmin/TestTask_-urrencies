using System.IO;
using System.Xml.Serialization;

namespace TestTask_Сurrencies.Models
{
    public class Xml_Serializer<T> : ISerializer<T>
    {
        public T DeserializeFromString(string input)
        {
            var serializer = new XmlSerializer(typeof(T));
            T result;

            using (TextReader reader = new StringReader(input))
            {
                result = (T)serializer.Deserialize(reader);
            }

            return result;
        }
    }
}
