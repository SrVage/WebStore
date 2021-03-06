using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities.Base;
using WebStore.Domain.Entities.Base.Interfaces;

namespace WebStore.Domain.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Section:NamedEntity, IOrderedEntity
{
    public int Order { get; set; }
    public int? ParentID { get; set; }
    [ForeignKey(nameof(ParentID))]
    public Section ParentSection { get; set; }
    
    public ICollection<Product> Products { get; set; }
}