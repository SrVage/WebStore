namespace WebStore.Domain.ViewModels;

public class CartViewModel
{
    public IEnumerable<(ProductViewModel product, int quantity)> Items { get; set; }
    public int ItemsCount => Items.Sum(p => p.quantity);
    public decimal TotalPrice => Items.Sum(p => p.product.Price * p.quantity);
}
