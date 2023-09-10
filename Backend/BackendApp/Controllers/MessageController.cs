using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApp.Interfaces;
using BackendApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApp.Controllers {
  [Route("api/message")]
  [ApiController]
  [Authorize(AuthenticationSchemes = "Bearer")]
  public class MessageController : ControllerBase {
    private readonly IMessageRepository _messageRepository;
    private readonly ILoginRepository _loginRepository;
    private readonly IChannelRepository _channelRepository;

    public MessageController(IMessageRepository messageRepository, ILoginRepository loginRepository,
      IChannelRepository channelRepository) {
      _messageRepository = messageRepository;
      _loginRepository = loginRepository;
      _channelRepository = channelRepository;
    }


    [HttpPost]
    public IActionResult Post([FromBody] CreateMessage createMessage) {
      try {
        string username = _loginRepository.DecodeJwtToken(Request.Headers["Authorization"][0].Split(" ")[1]);
        int userId = _loginRepository.GetUser(username).id;
        if (!_channelRepository.DoesChannelExistForUser(createMessage.channelId, userId)) return NotFound();

        _messageRepository.addMessage(userId, createMessage.channelId, createMessage.text);

        return Ok();
      }
      catch (Exception e) {
        return BadRequest($"Error: {e.Message}");
      }
    }
  }
}