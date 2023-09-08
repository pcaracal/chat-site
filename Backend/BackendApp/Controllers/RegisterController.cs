using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackendApp.Controllers {
  [Route("api/register")]
  [ApiController]
  public class RegisterController : ControllerBase {
    // POST: api/register
    [HttpPost]
    public IActionResult Post([FromBody] Login login) {
      // TODO: First search database if a user with same name exists, if so return 409 Conflict
      // TODO: Then hash password and save to database
      // TODO: Then return 201 Created


      // This is just to avoid errors in IDE for now
      return Ok();
    }
  }
}