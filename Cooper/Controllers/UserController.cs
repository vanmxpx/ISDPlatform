using Cooper.Models;
using Cooper.ORM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Cooper.Controllers
{
    public class UserController: ControllerBase
    {
        UserORM objuser = new UserORM();
        [HttpGet]
        [Route("api/User/Index")]
        public IEnumerable<User> Index()
        {
            return objuser.GetAll();
        }
        [HttpPost]
        [Route("api/User/Create")]
        public long Create([FromBody] User user)
        {
            return objuser.Add(user);
        }
        [HttpGet]
        [Route("api/User/Details/{id}")]
        public User Details(long id)
        {
            return objuser.GetData(id);
        }
        [HttpPut]
        [Route("api/User/Edit")]
        public int Edit([FromBody]User user)
        {
            return objuser.Update(user);
        }
        [HttpDelete]
        [Route("api/User/Delete/{id}")]
        public int Delete(long id)
        {
            return objuser.Delete(id);
        }
    }
}
