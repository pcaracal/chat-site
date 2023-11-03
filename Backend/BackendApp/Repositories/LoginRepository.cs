using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BackendApp.Interfaces;
using BackendApp.Models;
using Konscious.Security.Cryptography;
using Microsoft.IdentityModel.Tokens;

namespace BackendApp.Repositories;

public class LoginRepository : ILoginRepository {
  private readonly IConfiguration _configuration;
  private readonly ApplicationDbContext _context;

  public LoginRepository(ApplicationDbContext context,
                         IConfiguration configuration) {
    _context = context;
    _configuration = configuration;
  }

  public bool UserExists(string username) {
    return _context.user.FirstOrDefault(_login => _login.username ==
                                                  username) != null;
  }

  public Login GetUser(string username) {
    return _context.user.FirstOrDefault(_login => _login.username == username);
  }

  public void AddUser(Login login) {
    _context.user.Add(login);
    _context.SaveChanges();
  }

  public string GenerateArgon2Hash(string password) {
    // https://cheatsheetseries.owasp.org/cheatsheets/Password_Storage_Cheat_Sheet.html
    string _out;
    using (var hasher = new Argon2id(Encoding.UTF8.GetBytes(password))) {
      hasher.DegreeOfParallelism = 1;
      hasher.Iterations = 10;
      hasher.MemorySize = 19456;
      _out = Convert.ToBase64String(hasher.GetBytes(128));
    }

    return _out;
  }

  // Helper method to generate a JWT token
  public string GenerateJwtToken(string username) {
    var securityKey = new SymmetricSecurityKey(
        Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
    var credentials =
        new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

    var claims = new[] {
      new Claim(JwtRegisteredClaimNames.Sub, username),
      new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };

    var token = new JwtSecurityToken(
        _configuration["Jwt:Issuer"], _configuration["Jwt:Audience"], claims,
        expires: DateTime.UtcNow.AddHours(1), // Token expiration time
        signingCredentials: credentials);

    return new JwtSecurityTokenHandler().WriteToken(token);
  }

  // Helper method to get the username from a JWT token
  public string DecodeJwtToken(string token) {
    var handler = new JwtSecurityTokenHandler();
    var jsonToken = handler.ReadToken(token);
    var tokenS = jsonToken as JwtSecurityToken;
    var username = tokenS.Claims.First(claim => claim.Type == "sub").Value;
    return username;
  }
}
