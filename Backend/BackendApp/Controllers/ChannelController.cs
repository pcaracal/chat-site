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
  [Route("api/channel")]
  [ApiController]
  [Authorize(AuthenticationSchemes = "Bearer")]
  public class ChannelController : ControllerBase {
    private readonly IChannelRepository _channelRepository;
    private readonly ILoginRepository _loginRepository;

    public ChannelController(IChannelRepository channelRepository, ILoginRepository loginRepository) {
      _channelRepository = channelRepository;
      _loginRepository = loginRepository;
    }

    // TODO: POST api/channel adds an existing user to a channel
    // TODO: PATCH api/channel updates the channel name -- only admin can do this
    // TODO: DELETE api/channel deletes a channel -- only admin can do this
    // TODO: PUT api/channel deletes a user from a channel -- soft delete, user is only removed from channel but not from database

    // TODO: GET: api/channel returns all messages from a channel, all users from a channel, and the channel name
    // GET: api/channel/{channelId}
    [HttpGet("{channelId}")]
    public IActionResult Get(int channelId) {
      string username = _loginRepository.DecodeJwtToken(Request.Headers["Authorization"][0].Split(" ")[1]);
      int userId = _loginRepository.GetUser(username).id;
      if (!_channelRepository.DoesChannelExistForUser(channelId, userId)) return NotFound();

      string channelName = _channelRepository.GetChannelNameById(channelId);
      List<User> users = _channelRepository.GetUsersByChannelId(channelId);
      List<Message> messages = _channelRepository.GetMessagesByChannelId(channelId);

      return Ok(new ChannelData(channelName, users, messages));
    }
  }
}