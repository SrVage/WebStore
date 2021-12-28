using WebStore.Data;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services;

public class InMemoryProductData:IProductData
{
    public IEnumerable<Section> GetSection() => TestData.Sections;
    public IEnumerable<Brand> GetBrand() => TestData.Brands;
    public IEnumerable<Product> GetProduct(ProductFilter productFilter)
    {
        IEnumerable<Product> query = TestData.Products;
        if (productFilter is {SectionID: var sectionId})
            query = query.Where(p => p.SectionID == sectionId);
        if (productFilter is {BrandID: var brandId})
            query = query.Where(p => p.BrandID == brandId);
        return query;
    }
}