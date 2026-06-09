using Npgsql;
using Projekti.Common;
using Projekti.Common;
using Projekti.Common.Repository;
using Projekti.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekti.Repository
{
    public class UserModelRepository : IUserModelRepository
    {

        private readonly NpgsqlConnection _connection;


        private static List<UserModel> listOfUsers = new List<UserModel>();

        public UserModelRepository(NpgsqlConnection connection)
        {
            _connection = connection;
        }

        public async Task<UserModel> PostCreateUser(UserModel user)
        {
            try
            {
                string sql = "INSERT INTO \"UserModel\" (\"FirstName\", \"LastName\", \"Age\", \"Email\", \"Password\") VALUES (@FirstName, @LastName, @Age, @Email, @Password) RETURNING \"Id\"";

                if (_connection.State == ConnectionState.Closed)
                {
                    await _connection.OpenAsync();
                }
                using var command = new Npgsql.NpgsqlCommand(sql, _connection);

                command.Parameters.AddWithValue("@FirstName", user.FirstName);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Age", user.Age);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Password", user.Password);

                if (user.Articles == null)
                {
                    user.Articles = new List<Article>();
                }

                using var reader = await command.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    user.Id = reader.GetInt64(0);
                    listOfUsers.Add(user);
                }

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }


        public async Task<UserModel> GetSingleUserInfo(long id)
        {

            UserModel user = new UserModel();
            if (_connection.State == ConnectionState.Closed)
            {
                await _connection.OpenAsync();
            }
            using var command = new Npgsql.NpgsqlCommand("SELECT * FROM \"UserModel\" WHERE \"Id\" = @Id", _connection);

            command.Parameters.AddWithValue("@Id", id);

            using var reader = await command.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                user.FirstName = reader.GetString(reader.GetOrdinal("FirstName"));
                user.LastName = reader.GetString(reader.GetOrdinal("LastName"));
                user.Email = reader.GetString(reader.GetOrdinal("Email"));
                user.Age = reader.GetInt16(reader.GetOrdinal("Age"));
                user.Articles = new List<Article>();

                return user;
            }

            return null;
        }


        public async Task<Article> PostAddArticleUser(long id, Article article)
        {
            string sql = "INSERT INTO \"Article\" (\"Name\", \"Description\", \"CurrentPrice\", \"UserId\") VALUES (@Name, @Description, @CurrentPrice, @UserId)";



            if (_connection.State == ConnectionState.Closed)
            {
                await _connection.OpenAsync();
            }

            using var command = new Npgsql.NpgsqlCommand(sql, _connection);

            command.Parameters.AddWithValue("@Name", article.Name);
            command.Parameters.AddWithValue("@Description", article.Description);
            command.Parameters.AddWithValue("@CurrentPrice", article.CurrentPrice);
            command.Parameters.AddWithValue("@UserId", id);

            try
            {
                await command.ExecuteNonQueryAsync();
                return article;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public async Task<UserModel> PutUpdateUserInfo(long id, UserModel updatedData)
        {
            string sql = @"UPDATE ""UserModel""
                SET
            ""FirstName"" = @FirstName,
            ""LastName"" = @LastName,
            ""Age"" = @Age,
            ""Email"" = @Email,
            ""Password"" = @Password
                WHERE ""Id"" = @Id";



            using var command = new Npgsql.NpgsqlCommand(sql, _connection);

            command.Parameters.AddWithValue("@FirstName", updatedData.FirstName);
            command.Parameters.AddWithValue("@LastName", updatedData.LastName);
            command.Parameters.AddWithValue("@Age", updatedData.Age);
            command.Parameters.AddWithValue("@Email", updatedData.Email);
            command.Parameters.AddWithValue("@Password", updatedData.Password);
            command.Parameters.AddWithValue("@Id", id);

            try
            {
                if (_connection.State == ConnectionState.Closed)
                {
                    await _connection.OpenAsync();
                }

                int effectedRows = await command.ExecuteNonQueryAsync();

                if (effectedRows > 0)
                {
                    updatedData.Id = id;
                    return updatedData;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }



        public Article PutUpdateArticleUser(long userId, long articleId, Article updatedArticle)
        {
            UserModel user = listOfUsers.FirstOrDefault(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }

            Article article = user.Articles.FirstOrDefault(a => a.Id == articleId);
            if (article == null)
            {
                return null;
            }

            article.Name = updatedArticle.Name;
            article.Description = updatedArticle.Description;
            article.CurrentPrice = updatedArticle.CurrentPrice;

            article.Id = articleId;
            article.UserId = userId;

            return article;
        }



        public async Task<bool> DeleteSingleUserInfo(long id)
        {
            try
            {

                if (_connection.State == ConnectionState.Closed)
                {
                    await _connection.OpenAsync();
                }



                string sql = $"DELETE FROM \"UserModel\" WHERE \"Id\" = {id}";

                Npgsql.NpgsqlCommand command = new Npgsql.NpgsqlCommand(sql, _connection);


                command.Parameters.AddWithValue("@Id", id);

                int rowsAffected = await command.ExecuteNonQueryAsync();


                if (rowsAffected >= 0)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return false;
        }

        public async Task<List<UserModel>> GetFilteredUsers(short age = 0, string name = "", string lastName = "", long id = 0, string email = "", int numberOfArticles = 0)
        {
            List<UserModel> listOfUsers = new List<UserModel>();

            try
            {


                if (_connection.State == ConnectionState.Closed)
                {
                    await _connection.OpenAsync();
                }


                using var command = new NpgsqlCommand("", _connection);

                command.CommandText = UserFilter.FilterUsersSQL(command, age, name, lastName, id, email, numberOfArticles);


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
            return listOfUsers;
        }

    }
}
