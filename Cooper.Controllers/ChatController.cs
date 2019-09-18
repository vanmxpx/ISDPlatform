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
    public class MessageJSONDeserializedBody
    {
        public Message message;
        public IList<User> participants;
    }

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
        public IActionResult GetOneToOneChats()
        {
            string userToken = Request.GetUserToken();

            User user = userRepository.GetByJWToken(userToken);

            if (user == null)
            {
                return StatusCode(500, "Connection to database failed");
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
        public IActionResult Post([FromBody]MessageJSONDeserializedBody body)
        {
            Message message = body.message;
            IList<User> participants = body.participants;

            if ((message.Content == null) || (message.Content == ""))
            {
                return BadRequest();
            }

            message.CreateDate = System.DateTime.Now;

            if (message.ChatId == 0)
            {
                Chat chat = chatRepository.GetOnetoOneChatByParticipants(participants);

                if (chat == null)
                {
                    chat = new Chat() { Participants = participants };
                    chat.Id = chatRepository.Create(chat);

                    message.ChatId = chat.Id;
                    message.Id = messageRepository.Create(message);

                    chat.Messages = new List<Message>() { message };

                    hubContext.Clients.All.BroadcastChat(chat);
                }
                else
                {
                    message.ChatId = chat.Id;
                    message.Id = messageRepository.Create(message);

                    hubContext.Clients.All.BroadcastMessage(message);
                }

            }
            else
            {
                message.Id = messageRepository.Create(message);
                hubContext.Clients.All.BroadcastMessage(message);
            }

            return Ok(message);
        }

        [HttpPost("read-messages")]
        [Authorize]
        public IActionResult Post([FromBody]Chat chat)
        {

            if (chat == null)
            {
                return BadRequest();
            }

            bool messagesRead = messageRepository.ReadNewMessages(chat);

            if (!messagesRead)
            {
                return StatusCode(500,"Connection to database failed");
            }

            return Ok();
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