namespace BackendApp.Interfaces;

public interface ILogin {
  string username { get; set; }
  string password { get; set; }

  int id { get; set; }

  DateTime created_at { get; set; }


  string ToString();
}