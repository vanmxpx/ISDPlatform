using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/chat/messages")]
    public class MessageController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Message> GetAllChatMessages(long chatId)
        {
            return new List<Message>();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Message Get(long id)
        {
            return new Message();
        }


        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody]Message message)
        {
            // DAO MISSED
            return Ok(message);
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
