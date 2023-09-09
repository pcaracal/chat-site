using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BackendApp.Models;
using BackendApp.Repositories;
using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace BackendApp.Controllers {
  [Route("api/register")]
  [ApiController]
  public class RegisterController : ControllerBase {
    private readonly ApplicationDbContext _context;

    public RegisterController(ApplicationDbContext context) {
      _context = context;
    }


    // POST: api/register
    [HttpPost]
    public IActionResult Post([FromBody] Login login) {
      // TODO: First search database if a user with same name exists, if so return 409 Conflict
      // TODO: Then hash password and save to database
      // TODO: Then return 201 Created
      if (_context.Set<Login>().Any(_login => _login.username == login.username)) {
        return Conflict("User already exists");
      }

      string hashedPassword = GenerateArgon2Hash(login.password);
      _context.user.Add(new Login(login.username, hashedPassword));
      _context.SaveChanges();

      return Created("api/register", "User created");
    }

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