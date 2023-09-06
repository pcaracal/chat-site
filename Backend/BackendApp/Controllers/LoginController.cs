using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BackendApp.Controllers {
  [Route("api/login")]
  [ApiController]
  public class LoginController : ControllerBase {
    private readonly IConfiguration _configuration;

    public LoginController(IConfiguration configuration) {
      _configuration = configuration;
    }

    // GET: api/login
    [HttpGet]
    public IEnumerable<string> Get() {
      return new string[] { "value1", "value2" };
    }

    // GET: api/login/5
    [HttpGet("{id}", Name = "Get")]
    public string Get(int id) {
      return "value";
    }

    // POST: api/login
    [HttpPost]
    public IActionResult Post([FromBody] Login login) {
      // TODO: Replace this with searching a database later
      // current test values: "user", "pw" hashed with SHA256
      if (!(login.Username == "04f8996da763b7a969b1028ee3007569eaf3a635486ddab211d512c85b9df8fb" && login.Password == "30c952fab122c3f9759f02a6d95c3758b246b4fee239957b2d4fee46e26170c4")) {
        return Unauthorized();
      }

      var token = GenerateJwtToken(login.Username);
      return Ok(new { token });
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

    // PUT: api/login/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value) {
    }

    // DELETE: api/login/5
    [HttpDelete("{id}")]
    public void Delete(int id) {
    }
  }
}