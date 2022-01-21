using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Domain.Entities.Identity;
using WebStore.Domain.Entities.Orders;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Services.Services.InSQL
{
    public class SqlOrderService : IOrderService
    {
        private readonly WebStoreDB _dataBase;
        private readonly UserManager<User> _userManager;

        public SqlOrderService(WebStoreDB dataBase, UserManager<User> userManager)
        {
            _dataBase = dataBase;
            _userManager = userManager;
        }

        public async Task<Order> CreateOrderAsync(string userName, CartViewModel cart, OrderViewModel viewModel, CancellationToken cancel = default)
        {
            var user = await _userManager.FindByNameAsync(userName).ConfigureAwait(false);

            if (user is null)
                throw new InvalidOperationException($"Пользователь с именем {userName} не найден в БД");

            await using var transaction = await _dataBase.Database.BeginTransactionAsync(cancel).ConfigureAwait(false);

            var order = new Order
            {
                User = user,
                Address = viewModel.Address,
                Phone = viewModel.Phone,
                Description = viewModel.Description,
            };

            var productsIds = cart.Items.Select(item => item.product.ID).ToArray();

            var cartProducts = await _dataBase.Products
               .Where(p => productsIds.Contains(p.ID))
               .ToArrayAsync(cancel)
               .ConfigureAwait(false);

            order.Items = cart.Items.Join(
                cartProducts,
                cartItem => cartItem.product.ID,
                cartProduct => cartProduct.ID,
                (cartItem, cartProduct) => new OrderItem
                {
                    Order = order,
                    Product = cartProduct,
                    Price = cartProduct.Price, // Место для скидки
                Quantity = cartItem.quantity,
                }).ToArray();

            await _dataBase.Orders.AddAsync(order, cancel).ConfigureAwait(false);

            await _dataBase.SaveChangesAsync(cancel).ConfigureAwait(false);

            await transaction.CommitAsync(cancel).ConfigureAwait(false);

            return order;
        }

        public async Task<Order?> GetOrderByIDAsync(int ID, CancellationToken cancel = default)
        {
            var order = await _dataBase.Orders
          .Include(o => o.User)
          .Include(o => o.Items)
          .ThenInclude(item => item.Product)
          .FirstOrDefaultAsync(o => o.ID == ID, cancel)
          .ConfigureAwait(false);

            return order;
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(string userName, CancellationToken cancel = default)
        {
            var orders = await _dataBase.Orders
           .Include(o => o.User)
           .Include(o => o.Items)
           .ThenInclude(item => item.Product)
           .Where(o => o.User.UserName == userName)
           .ToArrayAsync(cancel)
           .ConfigureAwait(false);
            return orders;
        }
    }
}
