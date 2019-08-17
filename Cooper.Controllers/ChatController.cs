using Cooper.Models;
using Cooper.Repositories;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cooper.Controllers
{
    [Route("api/chats")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository chatRepository;

        public ChatController(IConfigProvider configProvider)
        {
            chatRepository = new ChatRepository(configProvider);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Chat))]
        [ProducesResponseType(404)]
        public IActionResult GetChatById(long userId)
        {
            IList<Chat> chats = chatRepository.GetPersonalChatsByUserId(userId);

            if (chats == null)
            {
                return NotFound();
            }

            return Ok(chats);
        }
        
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