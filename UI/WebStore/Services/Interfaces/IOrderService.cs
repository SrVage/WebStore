using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;

namespace WebStore.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetUserOrdersAsync(string userName, CancellationToken cancel = default);
        Task<Order?> GetOrderByIDAsync(int ID, CancellationToken cancel = default);
        Task<Order> CreateOrderAsync(string userName, CartViewModel cart, OrderViewModel viewModel, CancellationToken cancel = default);
    }
}
