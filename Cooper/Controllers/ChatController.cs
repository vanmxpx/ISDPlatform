using Cooper.Models;
using Cooper.ORM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Cooper.Controllers
{
    public class ChatController: ControllerBase
    {
        ChatORM objchat = new ChatORM();
        [HttpGet]
        [Route("api/Chat/Index")]
        public IEnumerable<Chat> Index()
        {
            return objchat.GetAllEntities();
        }
        [HttpPost]
        [Route("api/Chat/Create")]
        public bool Create([FromBody] Chat chat)
        {
            return objchat.AddEntity(chat);
        }
        [HttpGet]
        [Route("api/Chat/Details/{id}")]
        public Chat Details(long id)
        {
            return objchat.GetEntityData(id);
        }
        [HttpPut]
        [Route("api/Chat/Edit")]
        public bool Edit([FromBody]Chat chat)
        {
            return objchat.UpdateEntity(chat);
        }
        [HttpDelete]
        [Route("api/Chat/Delete/{id}")]
        public bool Delete(long id)
        {
            return objchat.DeleteEntity(id);
        }
    }
}
