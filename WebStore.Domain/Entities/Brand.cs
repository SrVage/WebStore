using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities;
[Index(nameof(Name), IsUnique = true)]
//[Table("Brandz")]
public class Brand:NamedEntity, IOrderedEntity
{
    [Column("Brand Order")]
    public int Order { get; set; }
    public ICollection<Product> Products { get; set; }
}