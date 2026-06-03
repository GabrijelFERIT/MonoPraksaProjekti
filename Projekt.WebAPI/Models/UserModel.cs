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

        public List<Article> Articles { get; set; } = new List<Article>();

        public long ArticleId { get; set; }


        


    }
}
