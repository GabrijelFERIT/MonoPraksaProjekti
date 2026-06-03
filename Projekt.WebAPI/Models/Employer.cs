namespace Projekt.WebAPI.Models
{
    public class Employer
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public short StreetNumber { get; set; }

        public char StreetCode { get; set; }

        public int PostalCode { get; set; }

        public List<Employee> Employees { get; set; } = new List<Employee>();

    }
}
