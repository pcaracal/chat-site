using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackendApp.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;

namespace BackendApp.Controllers {
  [Route("api/user")]
  [ApiController]
  public class UserController : ControllerBase {
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository) {
      _userRepository = userRepository;
    }

    [HttpGet]
    public IActionResult GetAllUsers() {
      try {
        return Ok(_userRepository.GetAllUsers());
      }
      catch (Exception e) {
        return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
      }
    }
  }
}