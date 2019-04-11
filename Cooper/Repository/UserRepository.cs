using System;
using System.Collections.Generic;
using Cooper.Models;
using Cooper.DAO;   
using Cooper.DAO.Models;
using AutoMapper;

namespace Cooper.Repository
{
    public class UserRepository : IRepository<User>
    {
        private UserDAO userDAO;

        public UserRepository()
        {
            userDAO = new UserDAO();
        }

        public IEnumerable<User> GetAll()
        {
            List<UserDb> users = (List<UserDb>)userDAO.GetAll();

            List<User> users_newType = new List<User>();

            foreach (UserDb user in users)
            {
                //User user_newType = mapper.Map<User>(user);
                User user_newType = UserMap(user);

                users_newType.Add(user_newType);
            }

            return users_newType;
        }

        public User GetByNickname(string nickname)
        {
            UserDb user = userDAO.GetByNickname(nickname);

            User user_newTyped = Mapper.Map<User>(user);

            return user_newTyped;
        }

        public User GetByEmail(string email)
        {
            UserDb user = userDAO.GetByEmail(email);

            User user_newTyped = Mapper.Map<User>(user);

            return user_newTyped;
        }

        public User Get(long id)
        {
            UserDb user = userDAO.GetExtended(id);

            User user_newTyped = Mapper.Map<User>(user);
            //User user_newTyped = UserMap(user);

            return user_newTyped;
        }

        public long Create(User user)
        {
            UserDb userDb = UserMap(user);

            return userDAO.Save(userDb);
        }

        public void Update(User user)
        {
            UserDb userDb = UserMap(user);

            userDAO.Update(userDb);
        }

        public void Delete(long id)
        {
            userDAO.Delete(id);
        }
        
        #region Mapping
        private User UserMap(UserDb user)
        {
            User user_newType = new User();

            #region Transfer main attributes

            user_newType.Id = user.Id;
            user_newType.Name = user.Name;
            user_newType.Nickname = user.Nickname;
            user_newType.Password = user.Password;
            user_newType.PhotoURL = user.PhotoURL;
            user_newType.IsVerified = user.IsVerified;
            user_newType.IsCreator = user.IsCreator;
            user_newType.IsBanned = user.IsBanned;
            user_newType.EndBanDate = user.EndBanDate;
            user_newType.PlatformLanguage = user.PlatformLanguage;
            user_newType.PlatformTheme = user.PlatformTheme;

            #endregion

            #region Transfering interop attributes
            //EMPTY

            #endregion

            return user_newType;
        }

        private UserDb UserMap(User user)
        {
            UserDb user_newType = new UserDb();

            #region Transfer main attributes

            user_newType.Name = user.Name;
            user_newType.Nickname = user.Nickname;
            user_newType.Password = user.Password;
            user_newType.PhotoURL = user.PhotoURL;
            user_newType.IsVerified = user.IsVerified;
            user_newType.IsCreator = user.IsCreator;
            user_newType.IsBanned = user.IsBanned;
            user_newType.EndBanDate = user.EndBanDate;
            user_newType.PlatformLanguage = user.PlatformLanguage;
            user_newType.PlatformTheme = user.PlatformTheme;

            #endregion

            #region Transfering interop attributes
            //EMPTY

            #endregion

            return user_newType;
        }

        #endregion
    }
}