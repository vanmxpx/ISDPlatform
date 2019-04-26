using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cooper.Models;
using Cooper.Repository;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cooper.Controllers
{
    [Route("api/chats")]
    public class ChatController : ControllerBase
    {
        ChatRepository chatRepository;

        public ChatController()
        {
            chatRepository = new ChatRepository();
        }

        [HttpGet]
        public IEnumerable<Chat> GetAll()
        {
            return chatRepository.GetAll();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Chat))]
        [ProducesResponseType(404)]
        public IActionResult GetChatById(long id)
        {
            Chat chat = chatRepository.Get(id);
            if (chat == null)
            {
                return NotFound();
            }

            return Ok(chat);
        }
        
        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]Chat chat)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (chat.Id == 0)
            {
                long id = chatRepository.Create(chat);
                chat.Id = id;

                return Ok(chat);
            }
            else
            {
                chatRepository.Update(chat);

                return Ok(chat);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            chatRepository.Delete(id);
            return Ok();
        }
    }
}
