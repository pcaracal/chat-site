using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(AuthenticationSchemes = "Bearer")]
    public IEnumerable<string> Get() {
      return new string[] { "value1", "value2" };
    }

    // POST: api/login
    [HttpPost]
    public IActionResult Post([FromBody] Login login) {
      // TODO: Replace this with searching a database later
      string ePassword = GenerateSHA512(login.Password);

      Console.WriteLine($"Login attempt\nUsername: {login.Username}\nPassword: {ePassword}");

      // current test values: "user", "pw" in cleartext (clear -> SHA256 -> SHA512)
      string tempUsername = "user";
      string tempPassword =
        "e0465998226d5f0ddfa9f9b942614a7aa9071a7c1b6f6dd9ae4a5a3dee0b381be88514103481f095b69d317e582a17fb3988485258c2c74f3a624803db5d4289";

      if (!(login.Username == tempUsername && ePassword == tempPassword)) {
        Console.WriteLine("Login fail");
        return Unauthorized();
      }

      var token = GenerateJwtToken(login.Username);
      Console.WriteLine("Login success");
      return Ok(new { token, login.Username, ePassword });
    }

    // PUT: api/login/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value) {
    }

    // DELETE: api/login/5
    [HttpDelete("{id}")]
    public void Delete(int id) {
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

    // Helper method to generate a SHA512 hash
    private string GenerateSHA512(string input) {
      byte[] inputBytes = Encoding.UTF8.GetBytes(input);
      byte[] hashBytes = SHA512.HashData(inputBytes);
      StringBuilder sb = new StringBuilder();

      foreach (byte b in hashBytes) {
        sb.Append(b.ToString("x2"));
      }

      return sb.ToString();
    }
  }
}