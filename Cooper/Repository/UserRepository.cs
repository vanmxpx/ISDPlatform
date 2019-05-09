using System;
using System.Text;
using System.Collections.Generic;
using Cooper.Models;
using Cooper.DAO;   
using Cooper.DAO.Models;
using Cooper.Repository.Mapping;
using Cooper.Controllers.ViewModels;
using Newtonsoft.Json.Linq;
using System.Security.Claims;
using Cooper.Services;
using Cooper.Configuration;

namespace Cooper.Repository
{
    public class UserRepository : IRepository<User>
    {
        private UserDAO userDAO;
        private ModelsMapper mapper;

        private readonly IJwtHandlerService jwtService;

        public UserRepository(IJwtHandlerService jwtService, IConfigProvider configProvider)
        {
            userDAO = new UserDAO(configProvider);
            mapper = new ModelsMapper();

            this.jwtService = jwtService;
        }

        #region Checking methods

        public bool IfNicknameExists(string nickname)
        {
            return userDAO.IfNicknameExists(nickname);
        }
        
        public bool IfEmailExists(string email)
        {
            return userDAO.IfEmailExists(email);
        }

        public bool IfUserExists(long id)
        {
            return userDAO.IfUserExists(id);
        }

        public bool IfPasswordMatched(long id, string password)
        {
            return userDAO.IfPasswordCorrect(id, password);
        }

        public bool CheckCredentials(string nickname, string password)
        {
            return userDAO.CheckCredentials(nickname, password);
        }

        #endregion


        #region Main methods

        #region Get Methods
        
        public IEnumerable<User> GetAll()
        {
            List<UserDb> users = (List<UserDb>)userDAO.GetAll();

            List<User> users_newType = new List<User>();

            foreach (UserDb user in users)
            {
                User user_newType = mapper.Map(user);

                users_newType.Add(user_newType);
            }

            return users_newType;
        }

        public User GetByNickname(string nickname)
        {
            UserDb user = userDAO.GetByNickname(nickname);

            User user_newTyped = null;

            if (user != null)
            {
                user_newTyped = mapper.Map(user);
            }

            return user_newTyped;
        }

        public User GetByEmail(string email)
        {
            UserDb user = userDAO.GetByEmail(email);
            
            User user_newTyped = null;

            if (user != null)
            {
                user_newTyped = mapper.Map(user);
            }

            return user_newTyped;
        }

        public User GetByJWToken(string token)
        {
            string nickname = jwtService.GetPayloadAttributeValue("username", token);
            
            return GetByNickname(nickname);
        }

        public User Get(long id)
        {
            UserDb user = userDAO.GetExtended(id);
            User user_newTyped = null;

            if (user != null)
            {
                user_newTyped = mapper.Map(user);
            }

            return user_newTyped;
        }

        #endregion
        
        /// <summary>
        /// Creates a new user by traditional site registration procedure.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>Id of created user</returns>
        public long Create(UserRegistration user)
        {
            UserDb userDb = mapper.Map(user);

            return userDAO.Save(userDb);
        }

        /// <summary>
        /// Creates a new user With help of social media.
        /// </summary>
        /// <param name="user"></param>
        /// <returns>If of created user</returns>
        public long Create(User user)
        {
            UserDb userDb = mapper.Map(user);

            return userDAO.Save(userDb);
        }

        public void Update(User user)
        {
            UserDb userDb = mapper.Map(user);

            userDAO.Update(userDb);
        }

        public void Delete(long id)
        {
            userDAO.Delete(id);
        }

        #endregion

    }
}