using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.WebAPI.Models;
using Projekt.WebAPI.Test;
using System;
using System.Collections.ObjectModel;

namespace Projekt.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private static List<UserModel> listOfUsers = new List<UserModel>();

        string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=OsijekPraksa123.";

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
            UserModel user = new UserModel();

            using var connection = new Npgsql.NpgsqlConnection(connectionString);
            using var command = new Npgsql.NpgsqlCommand("SELECT * FROM \"UserModel\" WHERE \"Id\" = @Id", connection);

            command.Parameters.AddWithValue("@Id", id);

            connection.Open();
            using var reader = command.ExecuteReader();

            if (reader.Read())
            {
                user.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                user.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                user.Email = reader.GetString(reader.GetOrdinal("Email"));
                user.Age = reader.GetInt16(reader.GetOrdinal("Age"));
                user.Articles = new List<Article>();

                return Ok(user);
            }

            return NotFound(404);
        }

        //public UserModel PostCreateUser(string firstName, string lastName, long id, short age, string email, string password)
        //{
        //    UserModel newUser = new UserModel(firstName, lastName, id, age, email, password);
        //    listOfUsers.Add(newUser);
        //    return newUser;
        //}

        [HttpPost(Name = "createNewUser")]
        public IActionResult PostCreateUser([FromBody] UserModel user)
        {
            try
            {
                string sql = "INSERT INTO \"UserModel\" (\"FirstName\", \"LastName\", \"Age\", \"Email\", \"Password\") VALUES (@FirstName, @LastName, @Age, @Email, @Password) RETURNING \"Id\"";

                using var connection = new Npgsql.NpgsqlConnection(connectionString);
                using var command = new Npgsql.NpgsqlCommand(sql, connection);

                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Age", user.Age);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);

                if (user.Articles == null)
                {
                    user.Articles = new List<Article>();
                }

                connection.Open();
                using var reader = command.ExecuteReader();

                if (reader.Read())
                {
                    user.Id = reader.GetInt64(0);
                    listOfUsers.Add(user);
                }

                return Ok(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return BadRequest(400);
        }

        [HttpPost("{id}/addarticle")]
        public IActionResult PostAddArticleUser(long id, [FromBody] Article article)
        {
            string sql = "INSERT INTO \"Article\" (\"Name\", \"Description\", \"CurrentPrice\", \"UserId\") VALUES (@Name, @Description, @CurrentPrice, @UserId)";

            using var connection = new Npgsql.NpgsqlConnection(connectionString);
            using var command = new Npgsql.NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@Name", article.Name);
            command.Parameters.AddWithValue("@Description", article.Description);
            command.Parameters.AddWithValue("@CurrentPrice", article.CurrentPrice);
            command.Parameters.AddWithValue("@UserId", id);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                return Ok(article);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult PutUpdateUserInfo(long id, [FromBody] UserModel updatedData)
        {
            string sql = @"UPDATE ""UserModel""
                SET
            ""FirstName"" = @FirstName,
            ""LastName"" = @LastName,
            ""Age"" = @Age,
            ""Email"" = @Email,
            ""Password"" = @Password
                WHERE ""Id"" = @Id";

            using var connection = new Npgsql.NpgsqlConnection(connectionString);
            using var command = new Npgsql.NpgsqlCommand(sql, connection);

            command.Parameters.AddWithValue("@FirstName", updatedData.FirstName);
            command.Parameters.AddWithValue("@LastName", updatedData.LastName);
            command.Parameters.AddWithValue("@Age", updatedData.Age);
            command.Parameters.AddWithValue("@Email", updatedData.Email);
            command.Parameters.AddWithValue("@Password", updatedData.Password);
            command.Parameters.AddWithValue("@Id", id);

            try
            {
                connection.Open();
                int effectedRows = command.ExecuteNonQuery();

                if (effectedRows > 0)
                {
                    updatedData.Id = id;
                    return Ok(updatedData);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return NotFound(404);
        }

        [HttpPut("{userId}/Article/{articleId}")]
        public IActionResult PutUpdateArticleUser(long userId, long articleId, [FromBody] Article updatedArticle)
        {
            UserModel user = listOfUsers.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return NotFound($"Korisnik s ID-jem {userId} nije pronađen.");
            }

            Article article = user.Articles.FirstOrDefault(a => a.Id == articleId);
            if (article == null)
            {
                return NotFound($"Artikl s ID-jem {articleId} ne postoji kod ovog korisnika.");
            }

            article.Name = updatedArticle.Name;
            article.Description = updatedArticle.Description;
            article.CurrentPrice = updatedArticle.CurrentPrice;

            article.Id = articleId;
            article.UserId = userId;

            return Ok(article);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSingleUserInfo(long id)
        {
            try
            {
                using var connection = new Npgsql.NpgsqlConnection(connectionString);



                string sql = $"DELETE FROM \"UserModel\" WHERE \"Id\" = {id}";

                Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(sql, connection);


                command.Parameters.AddWithValue("@Id", id);


                connection.Open();

                int rowsAffected = command.ExecuteNonQuery();


                if (rowsAffected >= 0)
                {
                    return Ok(200);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return NotFound(404);
        }

        [HttpGet(Name = "getFilteredUsers")]
        public IActionResult GetFilteredUsers(short age = 0, string name = "", string lastName = "", long id = 0, string email = "", int numberOfArticles = 0)
        {
            List<UserModel> listOfUsers = new List<UserModel>();

            try
            {
                using var connection = new Npgsql.NpgsqlConnection(connectionString);
                using var command = new Npgsql.NpgsqlCommand("SELECT * FROM \"UserModel\"", connection);

                connection.Open();
                using var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    UserModel user = new UserModel();
                    user.Id = reader.GetInt64(0);
                    user.FirstName = reader.GetString(1);
                    user.LastName = reader.GetString(2);
                    user.Age = reader.GetInt16(3);
                    user.Email = reader.GetString(4);
                    user.Password = reader.GetString(5);

                    user.Articles = new List<Article>();

                    listOfUsers.Add(user);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            List<UserModel> filteredUsers = listOfUsers
                .Where(user => age == 0 || user.Age > age)
                .Where(user => string.IsNullOrEmpty(name) || user.FirstName.Contains(name))
                .Where(user => string.IsNullOrEmpty(lastName) || user.LastName.Contains(lastName))
                .Where(user => id == 0 || user.Id == id)
                .Where(user => string.IsNullOrEmpty(email) || user.Email.Contains(email))
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