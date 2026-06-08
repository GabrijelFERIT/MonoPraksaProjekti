using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Projekti.Common
{
    public static class UserFilter
    {

        public static string FilterUsersSQL(Npgsql.NpgsqlCommand command , short age = 0, string name = "", string lastName = "", long id = 0, string email = "", int numberOfArticles = 0)
        {

            StringBuilder sbsql = new StringBuilder("SELECT \"Id\", \"FirstName\", \"LastName\", \"Age\", \"Email\", \"Password\" FROM \"UserModel\" WHERE 1=1");

            if (age != 0)
            {
                sbsql.Append($" AND \"Age\" > @Age");
                command.Parameters.AddWithValue("@Age", age);
            }
            if (!string.IsNullOrEmpty(name))
            {
                sbsql.Append($" AND \"FirstName\" ILIKE @FirstName");
                command.Parameters.AddWithValue("@FirstName", $"%{name}%");
            }
            if (!string.IsNullOrEmpty(lastName))
            {
                sbsql.Append($" AND \"LastName\" ILIKE @LastName");
                command.Parameters.AddWithValue("@LastName", $"%{lastName}%");
            }
            if (id > 0)
            {
                sbsql.Append($" AND \"Id\" = @Id");
                command.Parameters.AddWithValue("@Id", id);
            }
            if (!string.IsNullOrEmpty(email))
            {
                sbsql.Append($" AND \"Email\" ILIKE @Email");
                command.Parameters.AddWithValue("@Email", $"%{email}%");
            }

            return sbsql.ToString();

        }

    }
}
