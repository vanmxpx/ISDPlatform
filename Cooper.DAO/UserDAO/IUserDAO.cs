﻿using Cooper.DAO.Models;
using System.Collections.Generic;

namespace Cooper.DAO
{
    public interface IUserDAO
    {
        bool IfNicknameExists(string nickname);
        bool IfEmailExists(string email);
        bool IfPasswordCorrect(long id, string password);
        bool IfUserExists(long id);
        bool CheckCredentials(string nickname, string password);

        UserDb Get(object id);
        IList<UserDb> GetUsersById(IList<long> usersId);
        UserDb GetByNickname(string nickname);
        UserDb GetByEmail(string email);
        UserDb GetByUniqueAttribute(object attribute_value, string attribute_name);
        UserDb GetExtended(long id);
        IEnumerable<UserDb> GetAll();

        long Save(UserDb user);
        bool Delete(object id);
        bool Update(UserDb user, bool removePassword);
        bool Update(UserDb user);
        bool UpdateAvatar(string url, long userId);
    }
}
