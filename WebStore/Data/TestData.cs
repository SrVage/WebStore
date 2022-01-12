using WebStore.Domain.Entities;
//using WebStore.Models;

namespace WebStore.Data;

public static class TestData
{
    public static List<Employer> Employers { get; } = new()
    {
        new Employer {ID=1, LastName="Петров", FirstName="Иван", MiddleName="Федорович", Age = 36, TelephoneNumber=5453421, City="Москва" },
        new Employer {ID=2, LastName="Сидоров", FirstName="Кирилл", MiddleName="Андреевич", Age =32, TelephoneNumber=5423447, City="Королев" },
        new Employer {ID=3, LastName="Васин", FirstName="Александр", MiddleName="Алексеевич", Age =41, TelephoneNumber=2543724, City="Владимир" },
        new Employer {ID=4, LastName="Вениминов", FirstName="Илья", MiddleName="Альбертович", Age =26, TelephoneNumber=3373227, City="Краснодар" },
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
            new Section { ID = 19, Name = "Fendii", Order = 0, ParentID = 18 },
            new Section { ID = 20, Name = "Gues", Order = 1, ParentID = 18 },
            new Section { ID = 21, Name = "Valentin", Order = 2, ParentID = 18 },
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
        
        public static IEnumerable<Product> Products { get; } = new[]
        {
            new Product { ID = 1, Name = "Белое платье", Price = 1025, ImageURL = "product1.jpg", Order = 0, SectionID = 2, BrandID = 1 },
            new Product { ID = 2, Name = "Розовое платье", Price = 1025, ImageURL = "product2.jpg", Order = 1, SectionID = 2, BrandID = 1 },
            new Product { ID = 3, Name = "Красное платье", Price = 1025, ImageURL = "product3.jpg", Order = 2, SectionID = 2, BrandID = 1 },
            new Product { ID = 4, Name = "Джинсы", Price = 1025, ImageURL = "product4.jpg", Order = 3, SectionID = 2, BrandID = 1 },
            new Product { ID = 5, Name = "Лёгкая майка", Price = 1025, ImageURL = "product5.jpg", Order = 4, SectionID = 2, BrandID = 2 },
            new Product { ID = 6, Name = "Лёгкое голубое поло", Price = 1025, ImageURL = "product6.jpg", Order = 5, SectionID = 2, BrandID = 1 },
            new Product { ID = 7, Name = "Платье белое", Price = 1025, ImageURL = "product7.jpg", Order = 6, SectionID = 2, BrandID = 1 },
            new Product { ID = 8, Name = "Костюм кролика", Price = 1025, ImageURL = "product8.jpg", Order = 7, SectionID = 25, BrandID = 1 },
            new Product { ID = 9, Name = "Красное китайское платье", Price = 1025, ImageURL = "product9.jpg", Order = 8, SectionID = 25, BrandID = 1 },
            new Product { ID = 10, Name = "Женские джинсы", Price = 1025, ImageURL = "product10.jpg", Order = 9, SectionID = 25, BrandID = 3 },
            new Product { ID = 11, Name = "Джинсы женские", Price = 1025, ImageURL = "product11.jpg", Order = 10, SectionID = 25, BrandID = 3 },
            new Product { ID = 12, Name = "Летний костюм", Price = 1025, ImageURL = "product12.jpg", Order = 11, SectionID = 25, BrandID = 3 },
        };
}