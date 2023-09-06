namespace BackendApp;

public class Login {
  public string Username { get; set; }
  public string Password { get; set; }

  private int UserId { get; set; }

  public Login(string username, string password) {
    Username = username;
    Password = password;
  }

  public override string ToString() {
    return $"id: {UserId}, username: {Username}, password: {Password}";
  }
}