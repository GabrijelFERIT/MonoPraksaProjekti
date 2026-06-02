namespace Projekt.WebAPI.Models
{
    public class UserModel
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public long Id { get; set; }

        public short Age { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public UserModel(string firstName, string lastName, long id, short age, string email, string password) 
        {
        
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Id = id;
            this.Age = age;
            this.Email = email;
            this.Password = password;

        }


    }
}
