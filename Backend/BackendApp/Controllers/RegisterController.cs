using BackendApp.Interfaces;
using BackendApp.Models;
using Microsoft.AspNetCore.Mvc;


namespace BackendApp.Controllers {
  [Route("api/register")]
  [ApiController]
  public class RegisterController : ControllerBase {
    private readonly ILoginRepository _loginRepository;

    public RegisterController(ILoginRepository loginRepository) {
      _loginRepository = loginRepository;
    }


    // POST: api/register
    [HttpPost]
    public IActionResult Post([FromBody] Login login) {
      if (_loginRepository.UserExists(login.username)) {
        return Conflict("User already exists");
      }

      string hashedPassword = _loginRepository.GenerateArgon2Hash(login.password);
      _loginRepository.AddUser(new Login(login.username, hashedPassword));

      return Created("api/register", "User created");
    }
  }
}