using System;
using Microsoft.AspNetCore.Mvc;
using NuGet.Packaging;
using SimpleMvcSitemap;
using WebStore.Interfaces.Services;
namespace WebStore.Controllers.API
{
	public class SiteMapController:ControllerBase
	{
        public IActionResult Index([FromServices] IProductData ProductData)
        {
            var nodes = new List<SitemapNode>
            {
                new(Url.Action("Index", "Home")),
                new(Url.Action("ConfiguredAction", "Home")),
                new(Url.Action("Index", "Blogs")),
                new(Url.Action("Blog", "Blogs")),
                new(Url.Action("Index", "WebAPI")),
                new(Url.Action("Index", "Catalog")),
            };

            nodes.AddRange(ProductData.GetSection().Select(s => new SitemapNode(Url.Action("Index", "Catalog", new { SectionId = s.ID }))));

            foreach (var brand in ProductData.GetBrand())
                nodes.Add(new SitemapNode(Url.Action("Index", "Catalog", new { BrandId = brand.ID })));

            foreach (var product in ProductData.GetProduct())
                nodes.Add(new SitemapNode(Url.Action("Details", "Catalog", new { product.ID })));

            return new SitemapProvider().CreateSitemap(new SitemapModel(nodes));
        }
    }
}

