using WebStore.Domain;
using WebStore.Domain.Entities;

namespace WebStore.Services.Interfaces;

public interface IProductData
{
    IEnumerable<Section> GetSection();
    IEnumerable<Brand> GetBrand();

    IEnumerable<Product> GetProduct(ProductFilter? productFilter = null);

    Product? GetProductByID(int ID);
}