using BackendApp.Interfaces;

namespace BackendApp.Models;

public class Message : IMessage {
  public int id { get; set; }
  public string text { get; set; }
  public DateTime created_at { get; set; }
  public int fk_user_id { get; set; }
  public int fk_channel_id { get; set; }
}