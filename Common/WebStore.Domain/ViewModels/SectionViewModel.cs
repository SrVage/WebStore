namespace WebStore.Domain.ViewModels;

public class SectionViewModel
{
    public int ID { get; set; }
    public string Name { get; set; }
    public int Order { get; set; }
    public SectionViewModel? ParentSection { get; set; }
    public List<SectionViewModel> ChildSections { get; set; } = new List<SectionViewModel>();
}