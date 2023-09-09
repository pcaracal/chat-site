using BackendApp.Interfaces;
using BackendApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApp;

public class ApplicationDbContext : DbContext {
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
  }

  public DbSet<Login> user { get; set; }
  public DbSet<Channel> channel { get; set; }
  public DbSet<Message> message { get; set; }
  public DbSet<UserChannel> user_channel { get; set; }

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    base.OnModelCreating(modelBuilder);
  }
}