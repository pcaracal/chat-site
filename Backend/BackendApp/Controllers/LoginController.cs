using BackendApp.Interfaces;
using BackendApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendApp.Controllers {
  [Route("api/login")]
  [ApiController]
  public class LoginController : ControllerBase {
    private readonly ILoginRepository _loginRepository;

    public LoginController(ILoginRepository loginRepository) {
      _loginRepository = loginRepository;
    }

    // GET: api/login
    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public IActionResult Get() {
      string username = _loginRepository.DecodeJwtToken(Request.Headers["Authorization"][0].Split(" ")[1]);
      try {
        return Ok(new { username });
      }
      catch (Exception e) {
        return BadRequest($"Error: {e.Message}");
      }
    }

    // POST: api/login
    [HttpPost]
    public IActionResult Post([FromBody] Login login) {
      if (!_loginRepository.UserExists(login.username)) {
        return Unauthorized();
      }

      Login dbLogin = _loginRepository.GetUser(login.username);
      string hashedPassword = _loginRepository.GenerateArgon2Hash(login.password);

      if (hashedPassword != dbLogin.password) {
        return Unauthorized();
      }

      var token = _loginRepository.GenerateJwtToken(login.username);
      return Ok(new { token, Username = login.username });
    }

    // DELETE: api/login
    [HttpDelete]
    public void Delete() {
      // TODO: Invalidate token
    }
  }
}