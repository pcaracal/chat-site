using BackendApp.Models;

namespace BackendApp.Interfaces;

public interface ILoginRepository {
  bool UserExists(string username);
  Login GetUser(string username);

  void AddUser(Login login);

  string GenerateArgon2Hash(string password);

  string GenerateJwtToken(string username);

  string DecodeJwtToken(string token);
}