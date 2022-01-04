using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities
{
    [Index(nameof(Name))]
    public class Employer : NamedEntity
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int Age { get; set; }
        public int TelephoneNumber { get; set; }
        public string City { get; set; }
    }
}
