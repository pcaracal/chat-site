﻿namespace BackendApp.Interfaces;

public interface IUserChannel {
  int id { get; set; }
  int fk_user_id { get; set; }
  int fk_channel_id { get; set; }
}