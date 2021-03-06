using Microsoft.AspNetCore.Mvc;
using WebStore.Domain;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;
using WebStore.Services.Mapping;

namespace WebStore.Controllers;

public class CatalogController : Controller
{
    private readonly IProductData _productData;
    private readonly IConfiguration _configuration;

    public CatalogController(IProductData productData, IConfiguration configuration)
    {
        _productData = productData;
        _configuration = configuration;
    }
    public IActionResult Index(int? brandID, int? sectionID, int page=1, int? pageSize=null)
    {
        var pageSizes = pageSize ?? (int.TryParse(_configuration["CatalogPageSize"], out var value) ? value : null);
        var filter = new ProductFilter
        {
            BrandID = brandID,
            SectionID = sectionID,
            Page = page,
            PageSize = pageSizes
        };
        var (products, totalCount) = _productData.GetProduct(filter);
        var catalogModel = new CatalogViewModel
        {
            BrandID = brandID,
            SectionID = sectionID,
            Products = products.OrderBy(p=>p.Order).ToView(),
            PageViewModel = new()
            {
                Page = page,
                PageSize = pageSize ?? 0,
                TotalItems = totalCount
            }
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