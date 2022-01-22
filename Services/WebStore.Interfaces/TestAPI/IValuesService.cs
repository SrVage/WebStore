namespace WebStore.Interfaces.TestAPI
{
    public interface IValuesService
    {
        IEnumerable<string> GetValues();
        int Count();
        string? GetByID(int ID);
        void Add(string value);
        void Edit(int ID, string value);
        bool Delete(int ID);
    }
}
