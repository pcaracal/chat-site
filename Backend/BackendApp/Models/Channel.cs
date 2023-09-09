using System.ComponentModel.DataAnnotations;
using BackendApp.Interfaces;

namespace BackendApp.Models;

public class Channel : IChannel {
  [Key] public int id { get; set; }
  public string name { get; set; }
  public DateTime created_at { get; set; }
  public int fk_admin_id { get; set; }

  public Channel(string name, int fk_admin_id) {
    this.name = name;
    this.fk_admin_id = fk_admin_id;
    this.created_at = DateTime.Now.ToUniversalTime();
  }
}