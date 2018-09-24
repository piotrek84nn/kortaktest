namespace KotrakTest.Models
{
    public class Contractor : Person
    {
        public string CompanyName { get; set; }

        public Contractor(string id, string name, string surname, string companyName) : base (id, name, surname)
        {
            CompanyName = companyName;
        }

        public Contractor(string name, string surname, string companyName) : base(name, surname)
        {
            CompanyName = companyName;
        }
    }
}
