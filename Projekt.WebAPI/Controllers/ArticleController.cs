using Microsoft.AspNetCore.Mvc;
using Projekt.WebAPI.Models;
namespace Projekt.WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ArticleController
    {

        List<Article> articles = new List<Article>();

        [HttpGet(Name = "getArticleList")]

        public List<Article> GetArticleList()
        {

            if (articles.Count() == 0)
            {
                return null;
            }
        return articles;
        }

        [HttpGet("{id}")]

        public Article GetArticle(int id)
        {

            Article article = articles.FirstOrDefault(article => article.Id == id);

            return article;

        }


    }
}
