namespace WebStore.Domain.ViewModels;

public class CatalogViewModel
{
    public int? BrandID { get; set; }
    public int? SectionID { get; set; }
    public IEnumerable<ProductViewModel?> Products { get; set; } = null!;
    public PageViewModel PageViewModel { get; set; }
}