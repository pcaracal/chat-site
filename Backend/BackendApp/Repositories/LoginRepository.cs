using BackendApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApp.Repositories;

public class LoginRepository {
  private readonly DbContext _context;

  public LoginRepository(DbContext context) {
    _context = context;
  }

  public string GetPassword(string username) {
    var user = _context.Set<Login>().FirstOrDefault(u => u.username == username);

    if (user != null) {
      return user.password;
    }

    throw new InvalidOperationException("User not found");
  }
}