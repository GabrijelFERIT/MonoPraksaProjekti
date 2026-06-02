using Projekt.WebAPI.Models;

namespace Projekt.WebAPI.Test
{
    public class UserList
    {

        List<UserModel> ListOfUsers = new List<UserModel>();


        
        public List<UserModel> CreateUserList()
        {

            UserModel personOne = new UserModel("Marko", "Marković", 12345, 23, "mbox@mail.com", "12345");
            UserModel personTwo = new UserModel("Pero", "Perić", 12346, 27, "pp@mail.com", "12346");
            UserModel personThree = new UserModel("Gabrijel", "Barukčić", 12347, 29, "gb@mail.com", "12345");
            UserModel personFour = new UserModel("Ivo", "Ivić", 12348, 21, "ii@mail.com", "32345");

            
            ListOfUsers.Add(personOne);
            ListOfUsers.Add(personTwo);
            ListOfUsers.Add(personThree);
            ListOfUsers.Add(personFour);

            return ListOfUsers;

        }

    }
}
