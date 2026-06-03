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

        //[HttpGet(Name = "getUserInfo")]

        //public IActionResult GetAllUserInfo()
        //{
        //    if (listOfUsers.Count() <= 0)
        //    {
        //        return NotFound(404); 
        //    }
        //     return Ok(listOfUsers);

        //}

        [HttpGet("{id}")]

        public IActionResult GetSingleUserInfo(long id)
        {
            UserModel user = listOfUsers.FirstOrDefault(user => user.Id == id);
            if (user != null)
            {
                return Ok(user);
            }
            return NotFound(404);


        }

        [HttpPost(Name = "createNewUser")]

        //public UserModel PostCreateUser(string firstName, string lastName, long id, short age, string email, string password)
        //{

        //    UserModel newUser = new UserModel(firstName, lastName, id, age, email, password);

        //    listOfUsers.Add(newUser);

        //    return newUser;


        //}

        public IActionResult PostCreateUser(UserModel user)
        {

            UserModel newUser = user;

            listOfUsers.Add(newUser);


            return Ok(newUser);


        }


        [HttpPost("{id}/addarticle")]
        public IActionResult PostAddArticleUser(long id, [FromBody] Article article)
        {



            UserModel user = listOfUsers.FirstOrDefault(user => user.Id == article.UserId);
                if (user == null)
            {
                return NotFound(404);
            }
            
                user.Articles.Add(article);

                return Ok(user);
        }


        [HttpPut("{id}")]


        public IActionResult PutUpdateUserInfo(long id, [FromBody] UserModel updatedData)
        {


            UserModel user = listOfUsers.FirstOrDefault(user => user.Id == id);

            if (user.Id != null)
                {
                user.FirstName = updatedData.FirstName;
                user.LastName = updatedData.LastName;
                user.Age = updatedData.Age;
                user.Email = updatedData.Email;
                user.Password = updatedData.Password;

                return Ok(user);
                }

            return NotFound(404);
            
        }

        //[HttpPut("{id}/updataeArticle")]
        //public IActionResult PutUpdateArticleUser(long id, [FromBody] Article updatedArticle)
        //{
        //    UserModel user = listOfUsers.FirstOrDefault(user => user.Id == id);
        //    if (user == null)
        //    {
        //        return NotFound(404);
        //    }
        //    Article article = user.Articles.FirstOrDefault(article => article.Id == updatedArticle.Id);
        //    if (article == null)
        //    {
        //        return NotFound(404);
        //    }
        //    article.Name = updatedArticle.Name;
        //    article.CurrentPrice = updatedArticle.CurrentPrice;
        //    article.Description = updatedArticle.Description;
        //    return Ok(article);
        //}


        [HttpDelete("{id}")]

        public IActionResult DeleteSingleUserInfo(long id)
        {

            
            UserModel user = listOfUsers.FirstOrDefault(user => user.Id == id);


            if(user != null)
            {
                listOfUsers.Remove(user);
                return Ok(200);
            }
            return NotFound(404);

        }


        [HttpGet (Name = "getFilteredUsers")]
        public IActionResult GetFilteredUsers(short age = 0, string name = "", string lastName = "", long id = 0, string email = "", int numberOfArticles = 0)
        {
            List<UserModel> filteredUsers = listOfUsers
                .Where(user => age == 0 || user.Age > age)
                .Where(user => user.FirstName.Contains(name) || string.IsNullOrEmpty(name))
                .Where(user => user.LastName.Contains(lastName) || string.IsNullOrEmpty(user.LastName))
                .Where(user => id == 0 || user.Id == id)
                .Where(user => user.Email.Contains(email) || string.IsNullOrEmpty(user.Email))
                .Where(user => numberOfArticles == 0 || user.Articles.Count() > numberOfArticles)
                .ToList();


            if (filteredUsers.Count() <= 0)
            {
                return NotFound(404);
                
            }
            return Ok(filteredUsers);


        }


    }
}
