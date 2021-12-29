using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;
using WebStore.ViewModels;

namespace WebStore.Components;

public class BrandsViewComponent:ViewComponent
{
    private readonly IProductData _productData;

    public BrandsViewComponent(IProductData productData) 
        => _productData = productData;

    public IViewComponentResult Invoke() 
        => View(GetBrands());

    private IEnumerable<BrandViewModel> GetBrands()
        => _productData.GetBrand().OrderBy(b => b.Order).Select(b => new BrandViewModel
        {
            ID = b.ID,
            Name = b.Name
        });
}