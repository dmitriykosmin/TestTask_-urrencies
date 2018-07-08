using System.Net.Http;
using System.Threading.Tasks;

namespace TestTask_Сurrencies.Models
{
    public class DataHttpLoader : IDataWebLoader
    {
        public async Task<string> LoadDataToString(string source)
        {
            string result = "";

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(source))
            {
                result = await response.Content.ReadAsStringAsync();
            }

            return result;
        }
    }
}
