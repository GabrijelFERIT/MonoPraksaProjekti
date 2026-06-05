namespace Projekt.WebAPI.Models
{
    public class Employee
    {

        public long Id { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public long PhoneNumber { get; set; }

        public DateOnly EmploymentDate { get; set; }

        public long EmployerId { get; set; }

        public Employer Employer { get; set; } = new Employer();

    }
}
