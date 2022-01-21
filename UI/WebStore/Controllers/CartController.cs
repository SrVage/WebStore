using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        public readonly ICartService _cartService;

        public CartController(ICartService cartService) => _cartService = cartService;

        public IActionResult Index() => View(new CartOrderViewModel {Cart = _cartService.GetViewModel() });

        public IActionResult Add(int ID)
        {
            _cartService.Add(ID);
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Decrement(int ID)
        {
            _cartService.Decrement(ID);
            return RedirectToAction("Index", "Cart");
        }

        public IActionResult Remove(int ID)
        {
            _cartService.Remove(ID);
            return RedirectToAction("Index", "Cart");
        }

        [Authorize]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckOut(OrderViewModel viewModel, [FromServices] IOrderService orderService)
        {
            if (!ModelState.IsValid)
            {
                return View(nameof(Index), new CartOrderViewModel
                {
                    Cart = _cartService.GetViewModel(),
                    Order = viewModel
                });
            }
            var order = await orderService.CreateOrderAsync(User.Identity!.Name!, _cartService.GetViewModel(), viewModel);
            _cartService.Clear();
            return RedirectToAction(nameof(OrderConfirmed), new {order.ID});
        }

        public IActionResult OrderConfirmed(int ID)
        {
            ViewBag.OrderID = ID;
            return View();
        }
    }
}
