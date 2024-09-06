using System.Linq.Expressions;

namespace ProductManagment.API.Repository
{
    public interface IJsonFileHelper<T> where T : class
    {
        List<T> ReadFromJsonFile<T>();
        Task WriteToJsonFile<T>(List<T> data);


    }
}
