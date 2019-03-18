using Cooper.DAO;
using Cooper.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Cooper.Controllers
{
    public class ChatController: ControllerBase
    {
        ChatDataAccessLayer objchat = new ChatDataAccessLayer();
        [HttpGet]
        [Route("api/Chat/Index")]
        public IEnumerable<Chat> Index()
        {
            return objchat.GetAllChats();
        }
        [HttpPost]
        [Route("api/Chat/Create")]
        public bool Create([FromBody] Chat chat)
        {
            return objchat.AddChat(chat);
        }
        [HttpGet]
        [Route("api/Chat/Details/{id}")]
        public Chat Details(int id)
        {
            return objchat.GetChatData(id);
        }
        [HttpPut]
        [Route("api/Chat/Edit")]
        public bool Edit([FromBody]Chat chat)
        {
            return objchat.UpdateChat(chat);
        }
        [HttpDelete]
        [Route("api/Chat/Delete/{id}")]
        public bool Delete(int id)
        {
            return objchat.DeleteChat(id);
        }
    }
}
