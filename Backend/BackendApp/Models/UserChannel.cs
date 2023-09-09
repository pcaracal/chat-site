using System.ComponentModel.DataAnnotations;
using BackendApp.Interfaces;

namespace BackendApp.Models;

public class UserChannel : IUserChannel {
  [Key] public int id { get; set; }
  public int fk_user_id { get; set; }
  public int fk_channel_id { get; set; }

  public UserChannel(int fk_user_id, int fk_channel_id) {
    this.fk_user_id = fk_user_id;
    this.fk_channel_id = fk_channel_id;
  }
}