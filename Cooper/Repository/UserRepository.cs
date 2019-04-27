using System;
using System.Collections.Generic;
using Cooper.Models;
using Cooper.DAO;   
using Cooper.DAO.Models;
using Cooper.Repository.Mapping;
using Cooper.Controllers.ViewModels;


namespace Cooper.Repository
{
    public class UserRepository : IRepository<User>
    {
        private UserDAO userDAO;
        private ModelsMapper mapper;
        public UserRepository()
        {
            userDAO = new UserDAO();
            mapper = new ModelsMapper();
        }

        #region Checking methods

        public bool IfNicknameExists(string nickname)
        {
            return userDAO.IfNicknameExists(nickname);
        }

        public bool IfEmailExists(string email)             // needed for sending email
        {
            return userDAO.IfEmailExists(email);
        }

        public bool IfUserExists(long id)
        {
            return userDAO.IfUserExists(id);
        }

        public bool IfPasswordMatched(long id, string password)   // needed for changing password
        {
            return userDAO.IfPasswordCorrect(id, password);
        }

        public bool CheckCredentials(string nickname, string password)  // needed for authentification
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
                //User user_newType = mapper.Map<User>(user);
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
        
        public long Create(UserRegistration user)
        {
            UserDb userDb = mapper.Map(user);

            return userDAO.Save(userDb);
        }

        public long Create(User user)               // creation for Facebook/Google user
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