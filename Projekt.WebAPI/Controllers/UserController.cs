using Microsoft.AspNetCore.Mvc;
using System;
using Projekt.WebAPI.Models;
using Projekt.WebAPI.Test;
using System.Collections.ObjectModel;

namespace Projekt.WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {

    
        private static List<UserModel> listOfUsers = new List<UserModel>();

        static UserController()
        {
            UserList userList = new UserList();
            listOfUsers = userList.CreateUserList();
        }

        [HttpGet(Name = "getUserInfo")]

        public List<UserModel> GetAllUserInfo()
        {
            if (listOfUsers.Count() <= 0)
            {
                return null; 
            }
             return listOfUsers;

        }

        [HttpGet("{id}")]

        public UserModel GetSingleUserInfo(long id)
        {
            UserModel user =listOfUsers.FirstOrDefault(user => user.Id == id);

 
            return user;

        }

        [HttpPost(Name = "createNewUser")]

        //public UserModel PostCreateUser(string firstName, string lastName, long id, short age, string email, string password)
        //{

        //    UserModel newUser = new UserModel(firstName, lastName, id, age, email, password);

        //    listOfUsers.Add(newUser);

        //    return newUser;


        //}

        public UserModel PostCreateUser(UserModel user)
        {

            UserModel newUser = user;

            listOfUsers.Add(newUser);

            return newUser;


        }



        [HttpPut("{id}")]


        public string PutUpdateUserInfo(long id, [FromBody] UserModel updatedData)
        {
            string message = "Something went wrong";
            foreach (var user in listOfUsers)
            {
                if (user.Id == id)
                {
                    user.FirstName = updatedData.FirstName;
                    user.LastName = updatedData.LastName;
                    user.Age = updatedData.Age;
                    user.Email = updatedData.Email;
                    user.Password = updatedData.Password;

                    message = $"Successfully updated data for {user.FirstName} {user.LastName}, code 200";
                    return message;
                }

            }
            return message;
            
        }


        [HttpDelete("{id}")]

        public string DeleteSingleUserInfo(long id)
        {

            string message = "Something went terribly wrong!";
            foreach (var user in listOfUsers)
            {
                if (user.Id == id)
                {
                    listOfUsers.Remove(user);
                    return message = $"Successfully deleted user with id {id}, code 200";
                }
            }
            return message = $"User with id {id} was not found, code 404";
        }



    }
}
