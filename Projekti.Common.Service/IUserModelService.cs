using Projekti.Model;

namespace Projekti.Common.Service
{
    public interface IUserModelService
    {
        public Task<List<UserModel>> GetUserModelList(short age = 0, string name = "", string lastName = "", long id = 0, string email = "", int numberOfArticles = 0);

        public Task<UserModel> GetSingleUser(long id);

        public Task<UserModel> CreateUser(UserModel user);

        public Task<UserModel> UpdateUser(long id, UserModel user);

        public Task<Article> UpdateArticleInfo(long articleId, long userId, Article article);

        public Task<bool> DeleteSingleUser(long id);

        public Task<Article> AddArticleToUser(long userId, Article article);
    }
}
