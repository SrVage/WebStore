using WebStore.Domain;
using WebStore.Domain.Entities;

namespace WebStore.Services.Interfaces;

public interface IProductData
{
    IEnumerable<Section> GetSection();
    Section? GetSectionById(int Id);
    IEnumerable<Brand> GetBrand();
    Brand? GetBrandById(int Id);
    IEnumerable<Product> GetProduct(ProductFilter? productFilter = null);

    Product? GetProductByID(int ID);
    Product CreateProduct(string Name, int Order, decimal Price, string ImageUrl, string Section, string? Brand = null);
}