using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.ViewModels;
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
        var parentSections = sections.Where(s => s.ParentID is null);
        var parentSectionsViews = parentSections.Select(s => new SectionViewModel
        {
            ID = s.ID,
            Name = s.Name,
            Order = s.Order
        }).ToList();
        foreach (var parentSection in parentSectionsViews)
        {
            var childSections = sections.Where(s => s.ParentID == parentSection.ID);
            foreach (var childSection in childSections)
            {
                parentSection.ChildSections.Add(new SectionViewModel
                {
                    ID = childSection.ID,
                    Name = childSection.Name,
                    Order = childSection.Order,
                    ParentSection = parentSection
                });
                parentSection.ChildSections.Sort((a,b)
                    => Comparer<int>.Default.Compare(a.Order, b.Order));
            }
        }
        parentSectionsViews.Sort((a,b)
            => Comparer<int>.Default.Compare(a.Order, b.Order));
        return View(parentSectionsViews); 
    }
}