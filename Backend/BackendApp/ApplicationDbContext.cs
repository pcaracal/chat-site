using BackendApp.Interfaces;
using BackendApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BackendApp;

public class ApplicationDbContext : DbContext {
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder) {
    modelBuilder.Entity<UserChannel>().HasNoKey();
    base.OnModelCreating(modelBuilder);
  }

  public DbSet<Login> user { get; set; }
  public DbSet<Channel> channel { get; set; }
  public DbSet<Message> message { get; set; }
  public DbSet<UserChannel> user_channel { get; set; }
}