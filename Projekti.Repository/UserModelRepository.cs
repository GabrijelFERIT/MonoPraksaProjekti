using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekti.Model;
using Projekti.Common.Repository;
using Projekti.Common;
using Npgsql;
using Projekti.Common;

namespace Projekti.Repository
{
    public class UserModelRepository : IUserModelRepository
    {
        string connectionString = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=OsijekPraksa123.";
        private static List<UserModel> listOfUsers = new List<UserModel>();
        public UserModel PostCreateUser( UserModel user)
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

                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return null;
        }


        public UserModel GetSingleUserInfo(long id)
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

                return user;
            }

            return null;
        }


        public Article PostAddArticleUser(long id, Article article)
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
                return article;
            }
            catch (Exception e)
            {
                return null;
            }
        }


        public UserModel PutUpdateUserInfo(long id, UserModel updatedData)
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



        public bool DeleteSingleUserInfo(long id)
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
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            return false;
        }

        public List<UserModel> GetFilteredUsers(short age = 0, string name = "", string lastName = "", long id = 0, string email = "", int numberOfArticles = 0)
        {
            List<UserModel> listOfUsers = new List<UserModel>();

            try
            {
                using var connection = new Npgsql.NpgsqlConnection(connectionString);
                using var command = new NpgsqlCommand("", connection);

                command.CommandText = UserFilter.FilterUsersSQL(command, age, name, lastName, id, email, numberOfArticles);

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
            return listOfUsers;
        }

    }
}
