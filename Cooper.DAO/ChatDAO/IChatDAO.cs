using System;
using System.Collections.Generic;
using System.Text;
using Cooper.DAO.Models;

namespace Cooper.DAO
{
    public interface IChatDAO
    {
        IList<ChatDb> GetPersonalChats(long userId);
    }
}
