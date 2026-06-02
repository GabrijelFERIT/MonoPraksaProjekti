using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Projekt.WebAPI.Models;
namespace Projekt.WebAPI.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ArticleController : ControllerBase
    {

        List<Article> articles = new List<Article>();

        [HttpGet(Name = "getArticleList")]

        public IActionResult GetArticleList()
        {

            if (articles.Count() <= 0)
            {
                return NotFound(404);
            }
            return Ok(articles);

        }

        [HttpGet("{id}")]

        public IActionResult GetArticle(long id)
        {


            Article article = articles.FirstOrDefault(article => article.Id == id);

            if (article == null)
            {
                return NotFound(404);
            }

            return Ok(article);

        }




    }
}
