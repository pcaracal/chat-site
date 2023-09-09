using BackendApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApp.Repositories;

public class LoginRepository {
  private readonly DbContext _context;

  public LoginRepository(DbContext context) {
    _context = context;
  }

  public bool CheckIfUserExists(string username) {
    return _context.Set<Login>().Any(login => login.username == username);
  }

  public void AddUser(Login login) {
    _context.Set<Login>().Add(login);
    _context.SaveChanges();
  }
}