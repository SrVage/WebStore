﻿using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
using WebStore.Services.Interfaces;

namespace WebStore.Controllers
{
    public class UserProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Orders([FromServices] IOrderService orderService)
        {
            var orders = await orderService.GetUserOrdersAsync(User.Identity!.Name!);
            return View(orders.Select(order => new UserOrderViewModel
            {
                Id = order.ID,
                Address = order.Address,
                Phone = order.Phone,
                Description = order.Description, 
                TotalPrice = order.TotalPrice,
                Date = order.Date
            }));
        }
    }
}
