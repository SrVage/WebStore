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
}