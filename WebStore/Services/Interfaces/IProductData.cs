using WebStore.Domain.Entities;

namespace WebStore.Services.Interfaces;

public interface IProductData
{
    IEnumerable<Section> GetSection();
    IEnumerable<Brand> GetBrand();
}