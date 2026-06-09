using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekti.Model;
using Projekti.Common.Repository;
using Projekti.Common;
using System.Data;

namespace Projekti.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        List<Article> articles = new List<Article>();

        private readonly IDbConnection _connection;

        public List<Article> GetArticleList()
        {
            

            if (articles.Count() <= 0)
            {
                return null;
            }
            return articles;

        }


        public Article GetArticle(long id)
        {


            Article article = articles.FirstOrDefault(article => article.Id == id);

            if (article == null)
            {
                return null;
            }

            return article;

        }

    }
}
