﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.DAO.Models;

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
        UserDb GetByNickname(string nickname);
        UserDb GetByEmail(string email);
        UserDb GetByUniqueAttribute(object attribute_value, string attribute_name);
        UserDb GetExtended(long id);
        IEnumerable<UserDb> GetAll();

        long Save(UserDb user);
        void Delete(object id);
        void Update(UserDb user, bool removePassword);
        void Update(UserDb user);
    }
}
