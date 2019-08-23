using Cooper.Models;
using Cooper.Extensions;
using Cooper.Repositories;
using Cooper.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Cooper.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Cooper.Controllers
{
    [Route("api/chats")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ChatController : ControllerBase
    {
        private readonly IChatRepository chatRepository;
        private readonly IUserRepository userRepository;
        private readonly IMessageRepository messageRepository;

        private readonly IHubContext<ChatHub, ITypedHubClient> hubContext;

        public ChatController(IHubContext<ChatHub, ITypedHubClient> hubContext, IConfigProvider configProvider, IJwtHandlerService jwtHandlerService)
        {
            userRepository = new UserRepository(jwtHandlerService, configProvider);
            messageRepository = new MessageRepository(configProvider);
            chatRepository = new ChatRepository(configProvider, messageRepository, userRepository as IRepository<User>);

            this.hubContext = hubContext;
        }

        [Authorize]
        [HttpGet("one-to-one-chats")]
        [ProducesResponseType(200, Type = typeof(Chat))]
        [ProducesResponseType(404)]
        public IActionResult GetChatByUserId()
        {
            string userToken = Request.GetUserToken();

            User user = userRepository.GetByJWToken(userToken);

            if (user == null)
            {
                return StatusCode(500); //  connection to database failed
            }

            IList<Chat> chats = chatRepository.GetPersonalChatsByUserId(user.Id);

            if (chats == null)
            {
                return NotFound();
            }

            return Ok(chats);
        }

        [HttpPost("send-message")]
        [Authorize]
        public IActionResult Post([FromBody]Message message)
        {
            if ((message.Content == null) || (message.Content == ""))
            {
                return BadRequest();
            }

            message.Id = messageRepository.Create(message);
            hubContext.Clients.All.BroadcastMessage(message);

            return Ok();
        }

        [HttpPost("create-chat")]
        [Authorize]
        public IActionResult Post([FromBody]Chat chat)
        {
            long chatId = chatRepository.Create(chat);

            if (chatId == 0)
            {
                return BadRequest();
            }

            return Ok(chatId);
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