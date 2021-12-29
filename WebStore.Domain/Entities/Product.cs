using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities;

public class Product:NamedEntity, IOrderedEntity
{
    public int Order { get; set; }
    public int SectionID { get; set; }
    public int? BrandID { get; set; }
    public string ImageURL { get; set; }
    public decimal Price { get; set; }
}