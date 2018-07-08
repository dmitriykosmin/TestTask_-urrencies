
namespace TestTask_Сurrencies.Models
{
    public interface ISerializer<T>
    {
        T DeserializeFromString(string input);
    }
}
