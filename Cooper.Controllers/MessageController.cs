using Cooper.Models;
using Cooper.Repositories;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Cooper.Controllers
{
    [Route("api/chat/messages")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class MessageController : ControllerBase
    {
        private readonly MessageRepository messageRepository;

        public MessageController(IConfigProvider configProvider)
        {
            messageRepository = new MessageRepository(configProvider);
        }

        [HttpGet]
        public IEnumerable<Message> GetAll()
        {
            return messageRepository.GetAll();
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Message))]
        [ProducesResponseType(404)]
        public IActionResult GetMessageById(long id)
        {
            Message message = messageRepository.Get(id);
            if (message == null)
            {
                return NotFound();
            }

            return Ok(message);
        }

        // POST api/<controller>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(201)]
        public IActionResult Post([FromBody]Message message)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (message.Id == 0)
            {
                long id = messageRepository.Create(message);
                message.Id = id;

                return Ok(message);
            }
            else
            {
                messageRepository.Update(message);

                return Ok(message);
            }
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            messageRepository.Delete(id);
            return Ok();
        }
    }
}