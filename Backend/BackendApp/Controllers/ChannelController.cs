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


    // TODO: DELETE api/channel deletes a channel -- only admin can do this
    // TODO: PUT api/channel deletes a user from a channel -- soft delete, user is only removed from channel but not from database

    // TODO: GET: api/channel returns all messages from a channel, all users from a channel, and the channel name
    // GET: api/channel/{channelId}
    [HttpGet("{channelId}")]
    public IActionResult Get(int channelId) {
      try {
        string username = _loginRepository.DecodeJwtToken(Request.Headers["Authorization"][0].Split(" ")[1]);
        int userId = _loginRepository.GetUser(username).id;
        if (!_channelRepository.DoesChannelExistForUser(channelId, userId)) return NotFound();

        string channelName = _channelRepository.GetChannelNameById(channelId);
        List<User> users = _channelRepository.GetUsersByChannelId(channelId);
        List<Message> messages = _channelRepository.GetMessagesByChannelId(channelId);
        bool isAdmin = _channelRepository.IsUserAdmin(channelId, userId);

        return Ok(new ChannelData(channelName, users, messages, isAdmin));
      }
      catch (Exception e) {
        return BadRequest($"Error: {e.Message}");
      }
    }


    // POST: api/channel/{channelId}
    [HttpPost("{channelId}")]
    public IActionResult Post(int channelId, [FromBody] AddUser addUser) {
      try {
        string username = _loginRepository.DecodeJwtToken(Request.Headers["Authorization"][0].Split(" ")[1]);
        int userId = _loginRepository.GetUser(username).id;
        if (!_channelRepository.DoesChannelExistForUser(channelId, userId)) return NotFound();
        if (!_channelRepository.IsUserAdmin(channelId, userId)) return Unauthorized();

        if (!_loginRepository.UserExists(addUser.username)) return NotFound();
        // This fails if the user doesn't exist and returns a non explanatory error message
        int addUserId = _loginRepository.GetUser(addUser.username).id;

        if (_channelRepository.DoesChannelExistForUser(channelId, addUserId)) return NoContent();
        _channelRepository.AddUserToChannel(channelId, addUserId);

        return Ok();
      }
      catch (Exception e) {
        return BadRequest($"Error: {e.Message}");
      }
    }
    
    
    
    // TODO: PATCH api/channel updates the channel name -- only admin can do this
    // PATCH: api/channel/{channelId}
    [HttpPatch("{channelId}")]
    public IActionResult Patch(int channelId, [FromBody] UpdateChannel updateChannel) {
      try {
        string username = _loginRepository.DecodeJwtToken(Request.Headers["Authorization"][0].Split(" ")[1]);
        int userId = _loginRepository.GetUser(username).id;
        if (!_channelRepository.DoesChannelExistForUser(channelId, userId)) return NotFound();
        if (!_channelRepository.IsUserAdmin(channelId, userId)) return Unauthorized();

        _channelRepository.UpdateChannelName(channelId, updateChannel.name);
        
        return Ok();
      }
      catch (Exception e) {
        return BadRequest($"Error: {e.Message}");
      }
    }
  }
}