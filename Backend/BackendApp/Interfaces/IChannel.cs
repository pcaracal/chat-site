namespace BackendApp.Interfaces;

public interface IChannel {
  int id { get; set; }
  string name { get; set; }
  DateTime created_at { get; set; }
  int fk_admin_id { get; set; }
}