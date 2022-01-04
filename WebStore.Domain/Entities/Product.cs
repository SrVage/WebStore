using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities;

[Index(nameof(Name))]
public class Product:NamedEntity, IOrderedEntity
{
    public int Order { get; set; }
    public int SectionID { get; set; }
    public Section Section { get; set; }
    public int? BrandID { get; set; }
    public Brand Brand { get; set; }
    public string ImageURL { get; set; }
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
}