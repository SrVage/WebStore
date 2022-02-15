namespace WebStore.Domain;

public class ProductFilter
{
    public int? SectionID { get; set; }
    public int? BrandID { get; set; }
    public int[]? IDs { get; set; }
    public int Page { get; set; }
    public int? PageSize { get; set; }
}