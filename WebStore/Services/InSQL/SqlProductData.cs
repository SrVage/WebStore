using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain;
using WebStore.Domain.Entities;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InSQL
{
    public class SqlProductData : IProductData
    {
        private readonly WebStoreDB _dataBase;

        public SqlProductData(WebStoreDB dataBase)
        {
            _dataBase = dataBase;
        }

        public IEnumerable<Brand> GetBrand() => _dataBase.Brands;

        public IEnumerable<Product> GetProduct(ProductFilter? productFilter = null)
        {
            IQueryable<Product> query = _dataBase.Products;
            if (productFilter?.SectionID is { } section_id)
                query = query.Where(p => p.SectionID == section_id);
            if (productFilter?.BrandID is { } brand_id)
                query = query.Where(p => p.BrandID == brand_id);
            return query;
        }

        public Product? GetProductByID(int ID) => _dataBase.Products
            .Include(p=>p.Brand)
            .Include(p=>p.Section)
            .FirstOrDefault(p => p.ID == ID);

        public IEnumerable<Section> GetSection() => _dataBase.Sections;
    }
}
