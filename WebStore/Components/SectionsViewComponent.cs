using Microsoft.AspNetCore.Mvc;
using WebStore.Services.Interfaces;

namespace WebStore.Components;

public class SectionsViewComponent:ViewComponent
{
    private readonly IProductData _productData;

    public SectionsViewComponent(IProductData productData) 
        => _productData = productData;
    
    public IViewComponentResult Invoke()
    {
        var sections = _productData.GetSection();
        return View(sections);
        return View(); 
    }
}