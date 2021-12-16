using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        private static readonly List<Employer> _employers = new List<Employer>
        {
            new Employer(1, "Петров", "Иван", "Федорович", 36, 5453421, "Москва"),
            new Employer(2, "Сидоров", "Кирилл", "Андреевич", 32, 5423447, "Королев"),
            new Employer(3, "Васин", "Александр", "Алексеевич", 41, 2543724, "Владимир"),
            new Employer(4, "Вениминов", "Илья", "Альбертович", 26, 3373227, "Краснодар"),
        };

        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Employers()
        {
            return View(_employers);
        }

        public IActionResult EmployerID(int id)
        {
            return View(_employers[id-1]);
        }
    }
}
