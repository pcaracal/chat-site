using BackendApp.Models;

namespace BackendApp.Interfaces;

public interface IMessage {
  int id { get; set; }
  int fk_user_id { get; set; }
  string text { get; set; }
  DateTime created_at { get; set; }
  int fk_channel_id { get; set; }
  
}