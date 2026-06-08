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

        UserModelRepository userModelRepository = new UserModelRepository();

        public List<UserModel> GetUserModelList(short age = 0, string name = "", string lastName = "", long id = 0, string email = "", int numberOfArticles = 0)
        {
            var userList = userModelRepository.GetFilteredUsers(age, name, lastName, id, email, numberOfArticles);

            return userList ?? new List<UserModel>();
        }


        public UserModel GetSingleUser(long id)
        {
            var user = userModelRepository.GetSingleUserInfo(id);

            return user != null ? user : null;
        }


        public UserModel CreateUser(UserModel user)
        {
            var createdUser = userModelRepository.PostCreateUser(user);
            return createdUser != null ? createdUser : null;
        }


        public UserModel UpdateUser(long id, UserModel user)
        {
            var updatedUser = userModelRepository.PutUpdateUserInfo(id, user);
            return updatedUser != null ? updatedUser : null;
        }
        public Article UpdateArticleInfo(long articleId, long userId, Article article)
        {
            Article updatedArticle = userModelRepository.PutUpdateArticleUser(articleId, userId, article);
            return updatedArticle != null ? updatedArticle : null;
        }

        public bool DeleteSingleUser(long id)
        {
            return userModelRepository.DeleteSingleUserInfo(id)==true ? true : false;
        }


        public Article AddArticleToUser(long userId, Article article)
        {
            Article newArticle = userModelRepository.PostAddArticleUser(userId, article);
            return newArticle != null ? newArticle : null;
        }
    }
}