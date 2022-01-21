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

        public Product CreateProduct(string Name, int Order, decimal Price, string ImageUrl, string Section, string? Brand = null)
        {
            var section = _dataBase.Sections
                .FirstOrDefault(s => s.Name == Section)
                ?? new Section { Name = Section };
            var brand = Brand is { Length: > 0 }
                ? _dataBase.Brands
                .FirstOrDefault(b => b.Name == Brand) 
                ?? new Brand { Name = Brand }:null;
            var product = new Product
            {
                Name = Name,
                Price = Price,
                Order = Order,
                ImageURL = ImageUrl,
                Section = section,
                Brand = brand,
            };
            _dataBase.Products.Add(product);
            _dataBase.SaveChanges();
            return product;
        }

        public IEnumerable<Brand> GetBrand() => _dataBase.Brands;

        public Brand? GetBrandById(int Id)=> _dataBase.Brands
       .Include(b => b.Products)
       .FirstOrDefault(b => b.ID == Id);

        public IEnumerable<Product> GetProduct(ProductFilter? productFilter = null)
        {
            IQueryable<Product> query = _dataBase.Products
            .Include(p => p.Brand)
            .Include(p => p.Section);
            if(productFilter?.IDs?.Length>0)
            {
                query = query.Where(p => productFilter.IDs!.Contains(p.ID));
            }
            else
            {
                if (productFilter?.SectionID is { } section_id)
                    query = query.Where(p => p.SectionID == section_id);
                if (productFilter?.BrandID is { } brand_id)
                    query = query.Where(p => p.BrandID == brand_id);
            }
            return query;
        }

        public Product? GetProductByID(int ID) => _dataBase.Products
            .Include(p=>p.Brand)
            .Include(p=>p.Section)
            .FirstOrDefault(p => p.ID == ID);

        public IEnumerable<Section> GetSection() => _dataBase.Sections;

        public Section? GetSectionById(int Id) => _dataBase.Sections
       .Include(s => s.Products)
       .FirstOrDefault(s => s.ID == Id);
    }
}
