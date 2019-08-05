using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cooper.SignalR;
using Cooper.Configuration;
using Cooper.Extensions;
using Cooper.Models;
using Cooper.Repositories;
using Cooper.Repositories.CommonChats;
using Cooper.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Cooper.Services.Interfaces;

namespace Cooper.Controllers
{
    [Route("api/common")]
    [ApiController]
    public class CommonMessageController : ControllerBase
    {
        private IHubContext<ChatHub, ITypedHubClient> _hubContext;
        private UserRepository userRepository;
        private ICommonChatRepository commonChatRepository;
        public CommonMessageController(IHubContext<ChatHub, ITypedHubClient> hubContext, IConfigProvider configProvider, IJwtHandlerService jwtService, ICommonChatRepository commonChatRepository)
        {
            _hubContext = hubContext;
            userRepository = new UserRepository(jwtService, configProvider);
            this.commonChatRepository = commonChatRepository;
        }

        [HttpPost("send")]
        [Authorize]
        public IActionResult Post([FromBody]CommonMessages msg)
        {

            if ((msg.Text == null) || (msg.Text == "")) return BadRequest();

            User user = Request.GetAuthorizedUser(userRepository);
            int now = (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
            CommonMessage message = new CommonMessage {
                Content = msg.Text,
                CreateDate = now,
                Author = user };
            commonChatRepository.addMessage(message);
            _hubContext.Clients.All.BroadcastMessage(message);
            
            return Ok();
        }

        [HttpGet, Authorize]
        [ProducesResponseType(200, Type = typeof(CommonMessage))]
        public IActionResult getAllMessages()
        {
            return Ok(commonChatRepository.getMessages());
        }
        
    }
}