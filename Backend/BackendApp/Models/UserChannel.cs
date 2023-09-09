using BackendApp.Interfaces;

namespace BackendApp.Models;

public class UserChannel : IUserChannel {
  public int fk_user_id { get; set; }
  public int fk_channel_id { get; set; }
}