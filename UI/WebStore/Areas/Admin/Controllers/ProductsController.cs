using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebStore.Areas.Admin.ViewModels;
using WebStore.Domain.Entities.Identity;
using WebStore.Services.Interfaces;

namespace WebStore.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize(Roles = Role.Administrators)]
    public class ProductsController : Controller
    {
        private readonly IProductData _productData;

        public ProductsController(IProductData productData)
        {
            _productData = productData;
        }

        public IActionResult Index()
        {
            var products = _productData.GetProduct();
            return View(products);
        }

        public IActionResult Edit(int ID)
        {
            var product = _productData.GetProductByID(ID);

            if (product is null)
                return NotFound();
            return View(new EditProductViewModel
            {
                Id = product.ID,
                Name = product.Name,
                Order = product.Order,
                SectionId = product.SectionID,
                Section = product.Section.Name,
                Brand = product.Brand?.Name,
                BrandId = product.BrandID,
                ImageUrl = product.ImageURL,
                Price = product.Price,
            });
        }
        [HttpPost]
        public IActionResult Edit(EditProductViewModel Model)
        {
            if (!ModelState.IsValid)
                return View(Model);

            var product = _productData.GetProductByID(Model.Id);
            if (product is null)
                return NotFound();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int ID)
        {
            var product = _productData.GetProductByID(ID);

            if (product is null)
                return NotFound();

            return View(new EditProductViewModel
            {
                Id = product.ID,
                Name = product.Name,
                Order = product.Order,
                SectionId = product.SectionID,
                Section = product.Section.Name,
                Brand = product.Brand?.Name,
                BrandId = product.BrandID,
                ImageUrl = product.ImageURL,
                Price = product.Price,
            });
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int ID)
        {
            var product = _productData.GetProductByID(ID);
            if (product is null)
                return NotFound();
            return RedirectToAction(nameof(Index));
        }
    }
}
