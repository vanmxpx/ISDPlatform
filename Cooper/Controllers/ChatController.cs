using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/chats")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Chat> GetAllUserChats(long userId)
        {
            // MISSED DAO
            return new List<Chat>();
        }
        
        [HttpGet("{id}")]
        public Chat Get(long id)
        {
            // MISSED DAO
            return new Chat();
        }
        
        [HttpPost]
        public IActionResult Post([FromBody]Chat chat)
        {
            // DAO MISSED
            return Ok(chat);
        }
        
        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            // DAO MISSED
            return Ok();
        }
    }
}
