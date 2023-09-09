using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApp.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApp.Controllers {
  [Route("api/overview")]
  [ApiController]
  [Authorize(AuthenticationSchemes = "Bearer")]
  public class OverviewController : ControllerBase {
    private readonly IOverviewRepository _overviewRepository;
    private readonly ILoginRepository _loginRepository;

    public OverviewController(IOverviewRepository overviewRepository, ILoginRepository loginRepository) {
      _overviewRepository = overviewRepository;
      _loginRepository = loginRepository;
    }

    // GET: api/overview
    [HttpGet]
    public IActionResult Get() {
      string username = _loginRepository.DecodeJwtToken(Request.Headers["Authorization"][0].Split(" ")[1]);
      int userId = _loginRepository.GetUser(username).id;
      try {
        List<IChannel> channels = _overviewRepository.GetChannelsByUserId(userId);
        return Ok(new { channels });
      }
      catch (Exception e) {
        return BadRequest($"Error: {e.Message}");
      }
    }

    // POST: api/overview
    /// <summary>
    ///  Creates a new channel
    /// </summary>
    [HttpPost]
    public IActionResult Post([FromBody] string name) {
      string username = _loginRepository.DecodeJwtToken(Request.Headers["Authorization"][0].Split(" ")[1]);
      int userId = _loginRepository.GetUser(username).id;
      if (_overviewRepository.doesChannelExist(name)) return Conflict("Channel already exists");

      _overviewRepository.createChannel(name, userId);
      return Ok(name);
    }
  }
}