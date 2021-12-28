using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using WebStore.Domain;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

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
            Products = products.OrderBy(p=>p.Order).Select(p=>new ProductViewModel
            {
                ID = p.ID,
                Name = p.Name,
                Price = p.Price,
                ImageURL = p.ImageURL
            })
        };
        return View(catalogModel);
    }
}