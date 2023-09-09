using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BackendApp.Interfaces;
using BackendApp.Models;
using BackendApp.Repositories;
using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BackendApp.Controllers {
  [Route("api/login")]
  [ApiController]
  public class LoginController : ControllerBase {
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _context;
    private readonly ILoginRepository _loginRepository;

    public LoginController(IConfiguration configuration, ApplicationDbContext context,
      ILoginRepository loginRepository) {
      _configuration = configuration;
      _context = context;
      _loginRepository = loginRepository;
    }

    // GET: api/login
    [HttpGet]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public IActionResult Get() {
      string username = DecodeJwtToken(Request.Headers["Authorization"][0].Split(" ")[1]);

      try {
        var users = _context.user.ToList();
        return Ok(users);
      }
      catch (Exception e) {
        return BadRequest($"Error: {e.Message}");
      }
    }

    // POST: api/login
    [HttpPost]
    public IActionResult Post([FromBody] Login login) {
      if (_context.user.FirstOrDefault(_login => _login.username == login.username) == null) {
        return Unauthorized();
      }

      Login dbLogin = _context.user.First(_login => _login.username == login.username);
      string hashedPassword = GenerateArgon2Hash(login.password);

      if (hashedPassword != dbLogin.password) {
        return Unauthorized();
      }

      var token = GenerateJwtToken(login.username);
      return Ok(new { token, Username = login.username });
    }

    // DELETE: api/login
    [HttpDelete]
    public void Delete() {
      // TODO: Invalidate token
    }


    // Helper method to generate a JWT token
    private string GenerateJwtToken(string username) {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      var claims = new[] {
        new Claim(JwtRegisteredClaimNames.Sub, username),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
      };

      var token = new JwtSecurityToken(
        _configuration["Jwt:Issuer"],
        _configuration["Jwt:Audience"],
        claims,
        expires: DateTime.UtcNow.AddHours(1), // Token expiration time
        signingCredentials: credentials
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    // Helper method to generate a Bearer token to get a user's username which was used to generate the Bearer token
    private string DecodeJwtToken(string token) {
      var handler = new JwtSecurityTokenHandler();
      var jsonToken = handler.ReadToken(token);
      var tokenS = jsonToken as JwtSecurityToken;
      var username = tokenS.Claims.First(claim => claim.Type == "sub").Value;
      return username;
    }

    // Yes, the same method is in RegisterController.cs
    private string GenerateArgon2Hash(string password) {
      // https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html
      string _out;
      using (var hasher = new Argon2id(Encoding.UTF8.GetBytes(password))) {
        hasher.DegreeOfParallelism = 1;
        hasher.Iterations = 40;
        hasher.MemorySize = 19456;
        _out = Convert.ToBase64String(hasher.GetBytes(128));
      }

      return _out;
    }
  }
}