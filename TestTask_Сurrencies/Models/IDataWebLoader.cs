using System.Threading.Tasks;

namespace TestTask_Сurrencies.Models
{
    public interface IDataWebLoader
    {
        Task<string> LoadDataToString(string source);
    }
}
