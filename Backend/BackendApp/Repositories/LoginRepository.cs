using Microsoft.EntityFrameworkCore;

namespace BackendApp.Repositories;

public class LoginRepository {
  private readonly DbContext _context;

  public LoginRepository(DbContext context) {
    _context = context;
  }
}