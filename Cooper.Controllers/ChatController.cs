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
        private readonly Cooper.Services.Interfaces.ISession session;

        private readonly IHubContext<ChatHub, ITypedHubClient> hubContext;

        public ChatController(IHubContext<ChatHub, ITypedHubClient> hubContext, IJwtHandlerService jwtHandlerService, ISessionFactory sessionFactory)
        {
            session = sessionFactory.FactoryMethod();
            userRepository = new UserRepository(jwtHandlerService, session);
            messageRepository = new MessageRepository(session);
            chatRepository = new ChatRepository(messageRepository, userRepository as IRepository<User>, session);

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
                session.EndSession();
                return StatusCode(500, "Connection to database failed");
            }

            IList<Chat> chats = chatRepository.GetPersonalChatsByUserId(user.Id);
            session.EndSession();

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
            IActionResult result;

            Message message = body.message;
            IList<User> participants = body.participants;

            if ((message.Content == null) || (message.Content == ""))
            {
                return BadRequest();
            }

            message.CreateDate = System.DateTime.Now;

            bool isSuccessfull = true;

            session.StartSession();

            if (message.ChatId == 0)
            {
                Chat chat = chatRepository.GetOnetoOneChatByParticipants(participants);

                if (chat == null)
                {
                    chat = new Chat() { Participants = participants };
                    chat.Id = chatRepository.Create(chat);

                    isSuccessfull &= (chat.Id != 0);

                    message.ChatId = chat.Id;
                    message.Id = messageRepository.Create(message);

                    isSuccessfull = isSuccessfull && (message.Id != 0);

                    chat.Messages = new List<Message>() { message };

                    if (isSuccessfull)
                    {
                        session.Commit(endSession: true);
                        hubContext.Clients.All.BroadcastChat(chat);
                        result = Ok();
                    }
                    else
                    {
                        session.Rollback(endSession: true);
                        result = StatusCode(500, "Connection to database failed");
                    }
                }
                else
                {
                    message.ChatId = chat.Id;
                    message.Id = messageRepository.Create(message);

                    isSuccessfull &= (message.Id != 0);

                    if (isSuccessfull)
                    {
                        session.Commit(endSession: true);
                        hubContext.Clients.All.BroadcastMessage(message);
                        result = Ok();
                    }
                    else
                    {
                        session.Rollback(endSession: true);
                        result = StatusCode(500, "Connection to database failed");
                    }
                }

            }
            else
            {
                message.Id = messageRepository.Create(message);

                isSuccessfull &= (message.Id != 0);

                if (isSuccessfull)
                {
                    session.Commit(endSession: true);
                    hubContext.Clients.All.BroadcastMessage(message);
                    result = Ok();
                }
                else
                {
                    session.Rollback(endSession: true);
                    result = StatusCode(500, "Connection to database failed");
                }
            }

            return result;
        }

        [HttpPost("read-messages")]
        [Authorize]
        public IActionResult Post([FromBody]Chat chat)
        {
            IActionResult result;

            if (chat == null)
            {
                return BadRequest();
            }

            session.StartSession();

            bool messagesRead = messageRepository.ReadNewMessages(chat);

            if (messagesRead)
            {
                session.Commit(endSession: true);
                result = Ok();
            }
            else
            {
                session.Rollback(endSession: true);
                result = StatusCode(500, "Connection to database failed");
            }

            return result;
        }


        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            IActionResult result;
            session.StartSession();

            bool isDeleted = chatRepository.Delete(id);

            if (isDeleted)
            {
                session.Commit(endSession: true);
                result = Ok();
            }
            else
            {
                session.Rollback(endSession: true);
                result = StatusCode(500, "Connection to database failed");
            }
            return result;
        }
    }
}