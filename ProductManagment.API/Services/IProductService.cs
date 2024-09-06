namespace ProductManagment.API.Services
{
    public interface IProductService
    {
        Task<HttpResponseMessage> Add(Product product);
        Task<HttpResponseMessage> Update(Product product);
        Task<HttpResponseMessage> Delete(int id);
        List<Product> GetAll();
        Product GetById(int id);
    }
}
