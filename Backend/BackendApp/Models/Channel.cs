using BackendApp.Interfaces;

namespace BackendApp.Models;

public class Channel : IChannel {
  public int id { get; set; }
  public string name { get; set; }
  public DateTime created_at { get; set; }
  public int fk_admin_id { get; set; }
}