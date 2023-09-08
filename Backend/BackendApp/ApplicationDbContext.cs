using Microsoft.EntityFrameworkCore;

namespace BackendApp;

public class ApplicationDbContext : DbContext {
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
  }

  public DbSet<Login> Users { get; set; }
}