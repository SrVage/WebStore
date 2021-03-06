using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Components;

public class BrandsViewComponent:ViewComponent
{
    private readonly IProductData _productData;

    public BrandsViewComponent(IProductData productData) 
        => _productData = productData;

    public IViewComponentResult Invoke(string brandID)
    {
        ViewBag.BrandID = int.TryParse(brandID, out var id) ? id : (int?)null;
        return View(GetBrands());
    }

    private IEnumerable<BrandViewModel> GetBrands()
        => _productData.GetBrand().OrderBy(b => b.Order).Select(b => new BrandViewModel
        {
            ID = b.ID,
            Name = b.Name
        });
}