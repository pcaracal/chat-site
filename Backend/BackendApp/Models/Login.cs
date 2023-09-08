using System.ComponentModel.DataAnnotations;
using BackendApp.Interfaces;

namespace BackendApp.Models;

public class Login : ILogin {
  [Key] public int id { get; set; }
  public string username { get; set; }
  public string password { get; set; }

  public DateTime created_at { get; set; }

  public Login(string username, string password) {
    this.username = username;
    this.password = password;
  }

  public override string ToString() {
    return $"id: {id}, username: {username}, password: {password}";
  }
}