using Cooper.DAO;
using Cooper.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Cooper.Controllers
{
    public class UserController: ControllerBase
    {
        UserDataAccessLayer objuser = new UserDataAccessLayer();
        [HttpGet]
        [Route("api/User/Index")]
        public IEnumerable<User> Index()
        {
            return objuser.GetAllUsers();
        }
        [HttpPost]
        [Route("api/User/Create")]
        public bool Create([FromBody] User User)
        {
            return objuser.AddUser(User);
        }
        [HttpGet]
        [Route("api/User/Details/{id}")]
        public User Details(int id)
        {
            return objuser.GetUserData(id);
        }
        [HttpPut]
        [Route("api/User/Edit")]
        public bool Edit([FromBody]User User)
        {
            return objuser.UpdateUser(User);
        }
        [HttpDelete]
        [Route("api/User/Delete/{id}")]
        public bool Delete(int id)
        {
            return objuser.DeleteUser(id);
        }
    }
}
