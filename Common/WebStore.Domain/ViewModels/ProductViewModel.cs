namespace WebStore.Domain.ViewModels;

public class ProductViewModel
{
    public int ID { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ImageURL { get; set; }
    public string Section { get; set; }
    public string? Brand { get; set; }
}