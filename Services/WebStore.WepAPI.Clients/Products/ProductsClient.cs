using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Interfaces.Services;
using WebStore.WepAPI.Clients.Base;

namespace WebStore.WepAPI.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(HttpClient client, string address) : base(client, "api/products")
        {
        }

        public Product CreateProduct(string Name, int Order, decimal Price, string ImageUrl, string Section, string? Brand = null)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Brand> GetBrand()
        {
            throw new NotImplementedException();
        }

        public Brand? GetBrandById(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Product> GetProduct(ProductFilter? productFilter = null)
        {
            throw new NotImplementedException();
        }

        public Product? GetProductByID(int ID)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Section> GetSection()
        {
            throw new NotImplementedException();
        }

        public Section? GetSectionById(int Id)
        {
            throw new NotImplementedException();
        }
    }
}
