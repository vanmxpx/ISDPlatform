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
        UserDAO objuser = new UserDAO();
        [HttpGet]
        [Route("api/User/Index")]
        public IEnumerable<User> Index()
        {
            return objuser.GetAllEntities();
        }
        [HttpPost]
        [Route("api/User/Create")]
        public bool Create([FromBody] User user)
        {
            return objuser.AddEntity(user);
        }
        [HttpGet]
        [Route("api/User/Details/{id}")]
        public User Details(long id)
        {
            return objuser.GetEntityData(id);
        }
        [HttpPut]
        [Route("api/User/Edit")]
        public bool Edit([FromBody]User user)
        {
            return objuser.UpdateEntity(user);
        }
        [HttpDelete]
        [Route("api/User/Delete/{id}")]
        public bool Delete(long id)
        {
            return objuser.DeleteEntity(id);
        }
    }
}
