using Cooper.Controllers.ViewModels;
using Cooper.DAO;
using Cooper.DAO.Models;
using Cooper.Models;
using Cooper.Repositories.Mapping;
using Cooper.Services.Interfaces;
using System;
using System.Collections.Generic;

namespace Cooper.Repositories
{
    public class UserRepository : IRepository<User>, IUserRepository
    {
        private readonly IUserDAO userDAO;
        private readonly VerificationDAO verifyDAO;
        private readonly ModelsMapper mapper;
        private readonly IJwtHandlerService jwtService;

        public UserRepository(IJwtHandlerService jwtService, IConfigProvider configProvider)
        {
            userDAO = new UserDAO(configProvider);
            verifyDAO = new VerificationDAO(configProvider);
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

        public bool CheckVerifyByNickname(string nickname)
        {
            return userDAO.GetByNickname(nickname).Email.Contains("@");
        }

        public bool CheckVerifyByEmail(string email)
        {
            var result = userDAO.GetByEmail(email);
            return (result != null)? result.Email.Contains("@") : false;
        }

        public string GetVerifyEmail(string token)
        {
            return verifyDAO.Get(token)?.Email;
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

        public IList<User> GetUsersById(IList<long> usersId)
        {
            IList<User> users_newTyped = null;

            IList<UserDb> users = userDAO.GetUsersById(usersId);

            if (users != null)
            {
                users_newTyped = new List<User>(capacity: users.Count);

                foreach (var user in users)
                {
                    User user_newTyped = mapper.Map(user);

                    users_newTyped.Add(user_newTyped);
                }
            }

            return users_newTyped;
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

        public User Get(object obj)
        {
            long id = Convert.ToInt64(obj);
            UserDb user = userDAO.GetExtended(id);
            User user_newTyped = null;

            if (user != null)
            {
                user_newTyped = mapper.Map(user);
            }

            return user_newTyped;
        }

        public Login GetLogin(string email) {
            Login login = new Login();
            var user = userDAO.GetByEmail(email);
            login.Username = user.Nickname;
            login.Password = user.Password;

            return login;
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
            //Set default values
            userDb.Description = "I am just a man from nowhere. I like lorem ipsum and game of thrones.";
            userDb.PlatformLanguage = "English";
            userDb.PlatformTheme = "Light";

            return userDAO.Save(userDb);
        }

        public long Create(Verification verify) 
        {
            VerificationDb verifydb = mapper.Map(verify);

            return verifyDAO.Save(verifydb);
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

            userDAO.Update(userDb, removePassword: true);
        }

        public void ConfirmEmail(string token, string email)
        {
            var user = userDAO.GetByEmail(token);
            user.Email = email;
            userDAO.Update(user);
        }

        public void Delete(long id)
        {
            userDAO.Delete(id);
        }

        public void DeleteToken(string token)
        {
            verifyDAO.Delete(token);
        }

        #endregion

    }
}