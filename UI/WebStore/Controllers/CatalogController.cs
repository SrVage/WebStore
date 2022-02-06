using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers;

public class CatalogController : Controller
{
    private readonly IProductData _productData;

    public CatalogController(IProductData productData)
    {
        _productData = productData;
    }
    public IActionResult Index(int? brandID, int? sectionID)
    {
        var filter = new ProductFilter
        {
            BrandID = brandID,
            SectionID = sectionID
        };
        var products = _productData.GetProduct(filter);
        var catalogModel = new CatalogViewModel
        {
            BrandID = brandID,
            SectionID = sectionID,
            Products = products.OrderBy(p=>p.Order).ToView()
        };
        return View(catalogModel);
    }

    public IActionResult Details(int ID)
    {
        var product = _productData.GetProductByID(ID);
        if (product is null)
            return NotFound();
        return View(product.ToView());
    }
}