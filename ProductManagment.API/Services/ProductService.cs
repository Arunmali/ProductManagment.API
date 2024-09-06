
using ProductManagment.API.Repository;
using System.Net;

namespace ProductManagment.API.Services
{
    public class ProductService : IProductService
    {
        private readonly IJsonFileHelper<Product> _genericFileRepository;
        private readonly HttpResponseMessage _responseMessage;
        public ProductService(IJsonFileHelper<Product> genericFileRepository)
        {
            _genericFileRepository = genericFileRepository;
            _responseMessage = new HttpResponseMessage();

        }
        public async Task<HttpResponseMessage> Add(Product product)
        {
            try
            {
                List<Product> products = [];
                var existingProducts = _genericFileRepository.ReadFromJsonFile<Product>();
                product.Id = existingProducts.Count + 1;
                if (existingProducts != null)
                {
                    existingProducts.Add(product);
                    products = existingProducts;
                }
                else
                {
                    products.Add(product);
                }
                await _genericFileRepository.WriteToJsonFile(products).ConfigureAwait(false);

                _responseMessage.ReasonPhrase = "Product Added Successfully";
                _responseMessage.StatusCode = HttpStatusCode.Created;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _responseMessage;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            List<Product> products = _genericFileRepository.ReadFromJsonFile<Product>();

            Product productToRemove = products.Find(match: p => p.Id == id);

            if (productToRemove is not null)
            {
                products.Remove(productToRemove);
                await _genericFileRepository.WriteToJsonFile(products).ConfigureAwait(false);
                _responseMessage.ReasonPhrase = "Product removed successfully";
                _responseMessage.StatusCode = HttpStatusCode.OK;
                return _responseMessage;

            }

            _responseMessage.ReasonPhrase = "Product not found.";
            _responseMessage.StatusCode = HttpStatusCode.NotFound;
            return _responseMessage;
        }

        public List<Product> GetAll()
        {
            List<Product> products = new();
            try
            {
                products = _genericFileRepository.ReadFromJsonFile<Product>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return products;
        }

        public Product GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<HttpResponseMessage> Update(Product product)
        {
            List<Product> products = _genericFileRepository.ReadFromJsonFile<Product>();

            Product productToUpdate = products.Find(match: p => p.Id == product.Id);

            if (productToUpdate is not null)
            {
                productToUpdate.Price = product.Price;
                productToUpdate.Quantity = product.Quantity;
                productToUpdate.Name = product.Name;
                productToUpdate.Quantity = product.Quantity;
                productToUpdate.Id = product.Id;
                products.Remove(product);
                products.Add(productToUpdate);
                await _genericFileRepository.WriteToJsonFile(products).ConfigureAwait(false);

                _responseMessage.ReasonPhrase = "Product updated successfully";
                _responseMessage.StatusCode = HttpStatusCode.OK;
                return _responseMessage;

            }

            _responseMessage.ReasonPhrase = "Product not found.";
            _responseMessage.StatusCode = HttpStatusCode.NotFound;
            return _responseMessage;
        }
    }
}
