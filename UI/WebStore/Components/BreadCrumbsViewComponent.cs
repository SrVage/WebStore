using System;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
using WebStore.Interfaces.Services;

namespace WebStore.Components
{
	public class BreadCrumbsViewComponent : ViewComponent
	{
        private readonly IProductData _productData;
        public BreadCrumbsViewComponent(IProductData productData) => _productData = productData;

        public IViewComponentResult Invoke()
        {
            var model = new BreadCrumbsViewModel();

            if (int.TryParse(Request.Query["SectionId"], out var sectionId))
            {
                model.Section = _productData.GetSectionById(sectionId);
                if (model.Section?.ParentID is { } parentSectionId && model.Section.ParentSection is null)
                    model.Section.ParentSection = _productData.GetSectionById(parentSectionId)!;
            }

            if (int.TryParse(Request.Query["BrandId"], out var brandId))
                model.Brand = _productData.GetBrandById(brandId);

            if (int.TryParse(Request.RouteValues["id"]?.ToString(), out var product_id))
                model.Product = _productData.GetProductByID(product_id)?.Name;

            return View(model);
        }
    }
}
