using WebStore.Domain.Entities;

namespace WebStore.ViewModels;

public class CatalogViewModel
{
    public int? BrandID { get; set; }
    public int? SectionID { get; set; }
    public IEnumerable<ProductViewModel> Products { get; set; } = null!;
}