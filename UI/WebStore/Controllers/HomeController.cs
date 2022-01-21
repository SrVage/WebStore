using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Interfaces.Services;
using WebStore.Models;
using WebStore.Services.Mapping;

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
