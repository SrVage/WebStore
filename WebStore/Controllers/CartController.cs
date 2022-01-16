using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;

namespace WebStore.Controllers
{
    public class CartController : Controller
    {
        public readonly ICartService _cartService;

        public CartController(ICartService cartService) => _cartService = cartService;

        public IActionResult Index() => View(_cartService.GetViewModel());

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
    }
}
