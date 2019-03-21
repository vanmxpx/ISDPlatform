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
            return objchat.GetAll();
        }
        [HttpPost]
        [Route("api/Chat/Create")]
        public long Create([FromBody] Chat chat)
        {
            return objchat.Add(chat);
        }
        [HttpGet]
        [Route("api/Chat/Details/{id}")]
        public Chat Details(long id)
        {
            return objchat.GetData(id);
        }
        [HttpPut]
        [Route("api/Chat/Edit")]
        public int Edit([FromBody]Chat chat)
        {
            return objchat.Update(chat);
        }
        [HttpDelete]
        [Route("api/Chat/Delete/{id}")]
        public int Delete(long id)
        {
            return objchat.Delete(id);
        }
    }
}
