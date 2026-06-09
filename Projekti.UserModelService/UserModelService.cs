using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Projekti.Repository;
using Projekti.Model;
using Projekti.Common.Repository;
using Projekti.Common.Service;

namespace Projekti.Service
{



    public class UserModelService : IUserModelService
    {
        private readonly IUserModelRepository userModelRepository;

        public UserModelService(IUserModelRepository userModelRepository)
        {
            this.userModelRepository = userModelRepository;
        }

        public async Task<List<UserModel>> GetUserModelList(short age = 0, string name = "", string lastName = "", long id = 0, string email = "", int numberOfArticles = 0)
        {
            var userList = userModelRepository.GetFilteredUsers(age, name, lastName, id, email, numberOfArticles);

            return await userList ?? new List<UserModel>();
        }


        public async Task<UserModel> GetSingleUser(long id)
        {
            var user = await userModelRepository.GetSingleUserInfo(id);

            return  user != null ? user : null;
        }


        public async Task<UserModel> CreateUser(UserModel user)
        {
            var createdUser = await userModelRepository.PostCreateUser(user);
            return  createdUser != null ? createdUser : null;
        }


        public async Task<UserModel> UpdateUser(long id, UserModel user)
        {
            var updatedUser = await userModelRepository.PutUpdateUserInfo(id, user);
            return  updatedUser != null ? updatedUser : null;
        }
        public async Task<Article> UpdateArticleInfo(long articleId, long userId, Article article)
        {
            Article updatedArticle =  userModelRepository.PutUpdateArticleUser(articleId, userId, article);
            return  updatedArticle != null ? updatedArticle : null;
        }

        public async Task<bool> DeleteSingleUser(long id)
        {
            return await userModelRepository.DeleteSingleUserInfo(id)==true ? true : false;
        }


        public async Task<Article> AddArticleToUser(long userId, Article article)
        {
            Article newArticle = await userModelRepository.PostAddArticleUser(userId, article);
            return  newArticle != null ? newArticle : null;
        }
    }
}