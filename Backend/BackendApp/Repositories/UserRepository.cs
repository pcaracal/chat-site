using BackendApp.Interfaces;
using BackendApp.Models;

namespace BackendApp.Repositories;

public class UserRepository : IUserRepository {
  private readonly ApplicationDbContext _context;

  public UserRepository(ApplicationDbContext context) { _context = context; }

  public List<User> GetAllUsers() {
    List<Login> logins = _context.user.ToList();
    List<User> users = new List<User>();
    logins.ForEach(
        ln => { users.Add(new User(ln.id, ln.username, ln.created_at)); });
    return users;
  }
}
