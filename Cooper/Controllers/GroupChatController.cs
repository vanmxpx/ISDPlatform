using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cooper.SignalR;
using Cooper.Configuration;
using Cooper.Extensions;
using Cooper.Models;
using Cooper.Repository;
using Cooper.Repository.CommonChats;
using Cooper.Services;
using Cooper.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Cooper.Controllers
{
    [Route("api/group")]
    [ApiController]
    public class GroupChatController : ControllerBase
    {
        private IHubContext<GroupChatHub, IGroupChatClient> _hubContext;
        private UserRepository userRepository;
        private MessageRepository messageRepository;
        public GroupChatController(IHubContext<GroupChatHub, IGroupChatClient> hubContext, IConfigProvider configProvider, IJwtHandlerService jwtService, MessageRepository messageRepository)
        {
            _hubContext = hubContext;
            userRepository = new UserRepository(jwtService, configProvider);
            this.messageRepository = messageRepository;
        }

        [HttpPost("send")]
        [Authorize]
        public IActionResult Post([FromBody]Message msg)
        {

            if ((msg.Content == null) || (msg.Content == "")) return BadRequest();

            User user = Request.GetAuthorizedUser(userRepository);
            DateTime now = new DateTime();
            Message message = new Message
            {
                Content = msg.Content,
                CreateDate = now,
                IsRead = false,
                IdChat = msg.IdChat,
                IdSender = user
            };
            messageRepository.Create(msg);
            _hubContext.Clients.Group(msg.IdChat.ChatName).ReceiveMessage(message);
            
            return Ok();
        }

        [HttpGet("{chatid}"), Authorize]
        [ProducesResponseType(200, Type = typeof(Message))]
        public IActionResult getChatMessages(long chatid)
        {
            return Ok(messageRepository.GetSubset(chatid));
        }
        
    }
}