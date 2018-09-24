using System;

namespace KotrakTest
{
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }

        public Person (string id, string name, string surname)
        {
            Id = id;
            Name = name;
            Surname = surname;
        }

        public Person(string name, string surname)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
            Surname = surname;
        }
    }
}
