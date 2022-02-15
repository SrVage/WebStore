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
            var products = productData.GetProduct(new() {Page = 1, PageSize = 6}).Products
                .OrderBy(p => p.Order).ToView();
            ViewBag.Products = products;
            return View();
        }

        public string ConfiguredAction(string id, string Value1)
        {
            return $"Hello World! {id} - {Value1}";
        }

        public void Throw(string Message) => throw new ApplicationException(Message);



        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Status(string Code)
        {
            switch (Code)
            {
                default:
                    return Content($"Status code - {Code}");

                case "404":
                    return RedirectToAction(nameof(Error));
            }
        }
    }
}
