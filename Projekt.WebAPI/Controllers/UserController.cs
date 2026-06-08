using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Projekti.Model;
using System;
using System.Collections.ObjectModel;
using Projekti.Service;
using System.Runtime.CompilerServices;
using Projekti.Common.Service;


namespace Projekt.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private static List<UserModel> listOfUsers = new List<UserModel>();
        string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=OsijekPraksa123.";
        UserModelService service = new UserModelService();


        [HttpGet("{id}")]

        public ActionResult GetSingleUser(long id)
        {            
            return service.GetSingleUser(id) != null ? Ok(service.GetSingleUser(id)) : NotFound("No users found.");
        }

        [HttpPost(Name = "createNewUser")]

        public IActionResult CreateNewUser([FromBody] UserModel user)
        {
            var createdUser = service.CreateUser(user);
            if (createdUser != null)
            {
                return Ok(createdUser);
            }
            return BadRequest("Failed to create user.");
        }

        [HttpPost("{id}/addarticle")]

        public IActionResult AddArticleToUser(long id, [FromBody] Article article)
        {
            var updatedUser = service.AddArticleToUser(id, article);
            if (updatedUser != null)
            {
                return Ok(updatedUser);
            }
            return NotFound($"User with id {id} not found.");
        }

        [HttpPut("{id}")]

        public IActionResult UpdateSingleUser(long id, [FromBody] UserModel user)
        {
            var updatedUser = service.UpdateUser(id, user);
            if (updatedUser != null)
            {
                return Ok(updatedUser);
            }
            return NotFound($"User with id {id} not found.");
        }

        [HttpPut("{userId}/Article/{articleId}")]

        public IActionResult UpdateArticleInfo(long userId, long articleId, [FromBody] Article article)
        {
            var updatedArticle = service.UpdateArticleInfo(articleId, userId, article);
            if (updatedArticle != null)
            {
                return Ok(updatedArticle);
            }
            return NotFound($"Article with id {articleId} for user with id {userId} not found.");
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteSingleUser(long id)
        {
            if (service.DeleteSingleUser(id) == true)
            {
                return Ok($"User with id {id} has been deleted.");
            }
            return NotFound($"User with id {id} not found.");
        }

        [HttpGet(Name = "getFilteredUsers")]

        public IActionResult GetFilteredUsers(short age = 0, string name = "", string lastName = "", long id = 0, string email = "", int numberOfArticles = 0)
        {
            var users = service.GetUserModelList(age, name, lastName, id, email, numberOfArticles);
            if (users != null)
            {
                return Ok(users);
            }
            return NotFound("No users found.");
        }
    }
}