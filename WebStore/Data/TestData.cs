using WebStore.Domain.Entities;
using WebStore.Models;

namespace WebStore.Data;

public static class TestData
{
    public static List<Employer> Employers { get; } = new()
    {
        new Employer(1, "Петров", "Иван", "Федорович", 36, 5453421, "Москва"),
        new Employer(2, "Сидоров", "Кирилл", "Андреевич", 32, 5423447, "Королев"),
        new Employer(3, "Васин", "Александр", "Алексеевич", 41, 2543724, "Владимир"),
        new Employer(4, "Вениминов", "Илья", "Альбертович", 26, 3373227, "Краснодар"),
    };

    public static IEnumerable<Brand> Brands { get; } = new[]
        {
            new Brand { ID = 1, Name = "Acne", Order = 0 },
            new Brand { ID = 2, Name = "Grune Erde", Order = 1 },
            new Brand { ID = 3, Name = "Albiro", Order = 2 },
            new Brand { ID = 4, Name = "Ronhill", Order = 3 },
            new Brand { ID = 5, Name = "Oddmolly", Order = 4 },
            new Brand { ID = 6, Name = "Boudestijn", Order = 5 },
            new Brand { ID = 7, Name = "Rosch creative culture", Order = 6 },
        };
    

        public static IEnumerable<Section> Sections { get; } = new[]
        {
            new Section { ID = 1, Name = "Спорт", Order = 0 },
            new Section { ID = 2, Name = "Nike", Order = 0, ParentID = 1 },
            new Section { ID = 3, Name = "Under Armour", Order = 1, ParentID = 1 },
            new Section { ID = 4, Name = "Adidas", Order = 2, ParentID = 1 },
            new Section { ID = 5, Name = "Puma", Order = 3, ParentID = 1 },
            new Section { ID = 6, Name = "ASICS", Order = 4, ParentID = 1 },
            new Section { ID = 7, Name = "Для мужчин", Order = 1 },
            new Section { ID = 8, Name = "Fendi", Order = 0, ParentID = 7 },
            new Section { ID = 9, Name = "Guess", Order = 1, ParentID = 7 },
            new Section { ID = 10, Name = "Valentino", Order = 2, ParentID = 7 },
            new Section { ID = 11, Name = "Диор", Order = 3, ParentID = 7 },
            new Section { ID = 12, Name = "Версачи", Order = 4, ParentID = 7 },
            new Section { ID = 13, Name = "Армани", Order = 5, ParentID = 7 },
            new Section { ID = 14, Name = "Prada", Order = 6, ParentID = 7 },
            new Section { ID = 15, Name = "Дольче и Габбана", Order = 7, ParentID = 7 },
            new Section { ID = 16, Name = "Шанель", Order = 8, ParentID = 7 },
            new Section { ID = 17, Name = "Гуччи", Order = 9, ParentID = 7 },
            new Section { ID = 18, Name = "Для женщин", Order = 2 },
            new Section { ID = 19, Name = "Fendi", Order = 0, ParentID = 18 },
            new Section { ID = 20, Name = "Guess", Order = 1, ParentID = 18 },
            new Section { ID = 21, Name = "Valentino", Order = 2, ParentID = 18 },
            new Section { ID = 22, Name = "Dior", Order = 3, ParentID = 18 },
            new Section { ID = 23, Name = "Versace", Order = 4, ParentID = 18 },
            new Section { ID = 24, Name = "Для детей", Order = 3 },
            new Section { ID = 25, Name = "Мода", Order = 4 },
            new Section { ID = 26, Name = "Для дома", Order = 5 },
            new Section { ID = 27, Name = "Интерьер", Order = 6 },
            new Section { ID = 28, Name = "Одежда", Order = 7 },
            new Section { ID = 29, Name = "Сумки", Order = 8 },
            new Section { ID = 30, Name = "Обувь", Order = 9 },
        };
}