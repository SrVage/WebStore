using System.Net.Http.Json;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Domain.Entities;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;
using WebStore.WepAPI.Clients.Base;

namespace WebStore.WepAPI.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(HttpClient client, string address) : base(client, WebAPIAddresses.Products)
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

            var product = response.Content.ReadFromJsonAsync<ProductDTO>().Result;
            return product.FromDTO()!;
        }

        public IEnumerable<Brand> GetBrand()
            => Get<IEnumerable<BrandDTO>>($"{Address}/brands")!.FromDTO()!;

        public Brand? GetBrandById(int Id)
            => Get<BrandDTO>($"{Address}/brand/{Id}").FromDTO();

        public IEnumerable<Product> GetProduct(ProductFilter? productFilter = null)
            => Post(Address, productFilter ?? new()).Content.ReadFromJsonAsync<IEnumerable<ProductDTO>>().Result!.FromDTO()!;

        public Product? GetProductByID(int ID) 
            => Get<ProductDTO>($"{Address}/{ID}").FromDTO();

        public IEnumerable<Section> GetSection()
            => Get<IEnumerable<SectionDTO>>($"{Address}/sections")!.FromDTO()!;

        public Section? GetSectionById(int Id)
            => Get<SectionDTO>($"{Address}/section/{Id}").FromDTO();
    }
}
