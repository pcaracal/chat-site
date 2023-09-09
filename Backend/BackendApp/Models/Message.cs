using System.ComponentModel.DataAnnotations;
using BackendApp.Interfaces;

namespace BackendApp.Models;

public class Message : IMessage {
  [Key] public int id { get; set; }
  public string text { get; set; }
  public DateTime created_at { get; set; }
  public int fk_user_id { get; set; }

  public int fk_channel_id { get; set; }

  // All args constructor, id not included and created_at is set to DateTime.Now
  public Message(string text, int fk_user_id, int fk_channel_id) {
    this.text = text;
    this.fk_user_id = fk_user_id;
    this.fk_channel_id = fk_channel_id;
    created_at = DateTime.Now.ToUniversalTime();
  }
}