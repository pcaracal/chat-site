namespace BackendApp.Interfaces;

public interface IUser {
  int id { get; set; }
  string username { get; set; }
  DateTime created_at { get; set; }
}