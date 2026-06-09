using Projekti.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Projekti.Common.Repository
{
    public interface IUserModelRepository
    {

        public Task<UserModel> PostCreateUser(UserModel user);

        public Task<UserModel> GetSingleUserInfo(long id);

        public Task<Article> PostAddArticleUser(long id, Article article);

        public Task<UserModel> PutUpdateUserInfo(long id, UserModel updatedData);



        public Article PutUpdateArticleUser(long userId, long articleId, Article updatedArticle);

        public Task<bool> DeleteSingleUserInfo(long id);
        public Task<List<UserModel>> GetFilteredUsers(short age = 0, string name = "", string lastName = "", long id = 0, string email = "", int numberOfArticles = 0);

    }
}
