using System;
namespace WebStore.Models
{
    public class Employer
    {
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public int Age { get; set; }
        public int TelephoneNumber { get; set; }
        public string City { get; set; }

        public Employer(int iD, string lastName, string firstName, string middleName, int age, int telephoneNumber, string city)
        {
            ID = iD;
            LastName = lastName;
            FirstName = firstName;
            MiddleName = middleName;
            Age = age;
            TelephoneNumber = telephoneNumber;
            City = city;
        }
    }
}

