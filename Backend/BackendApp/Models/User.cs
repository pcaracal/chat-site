using BackendApp.Interfaces;

namespace BackendApp.Models;

public class User : IUser {
  public int id { get; set; }
  public string username { get; set; }
  public DateTime created_at { get; set; }

  public User(int id, string username, DateTime created_at) {
    this.id = id;
    this.username = username;
    this.created_at = created_at;
  }
}