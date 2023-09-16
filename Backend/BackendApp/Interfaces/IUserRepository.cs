using BackendApp.Models;

namespace BackendApp.Interfaces;

public interface IUserRepository {
  List<User> GetAllUsers();
}