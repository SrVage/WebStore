using System.Net.Http.Json;
using WebStore.Domain;
using WebStore.Domain.DTO;
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
            var response = Post($"{Address}/new", new CreateProductDTO
            {
                Name = Name,
                Order = Order,
                Price = Price,
                ImageUrl = ImageUrl,
                Section = Section,
                Brand = Brand,
            });

            var product = response.Content.ReadFromJsonAsync<Product>().Result;
            return product;
        }

        public IEnumerable<Brand> GetBrand()
            => Get<IEnumerable<Brand>>($"{Address}/brands");

        public Brand? GetBrandById(int Id)
            => Get<Brand>($"{Address}/brand/{Id}");

        public IEnumerable<Product> GetProduct(ProductFilter? productFilter = null)
            => Post(Address, productFilter ?? new()).Content.ReadFromJsonAsync<IEnumerable<Product>>().Result!;

        public Product? GetProductByID(int ID) 
            => Get<Product>($"{Address}/{ID}");

        public IEnumerable<Section> GetSection()
            => Get<IEnumerable<Section>>($"{Address}/sections");

        public Section? GetSectionById(int Id)
            => Get<Section>($"{Address}/section/{Id}");
    }
}
