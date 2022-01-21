using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Infrastructure.Mapping;
using WebStore.Models;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        
        // GET: /<controller>/
        public IActionResult Index([FromServices] IProductData productData)
        {
            var products = productData.GetProduct()
                .OrderBy(p => p.Order).Take(6).ToView();
            ViewBag.Products = products;
            return View();
        }
        
        public IActionResult Error()
        {
            return View();
        }
    }
}
