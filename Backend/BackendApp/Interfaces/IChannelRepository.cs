﻿using BackendApp.Models;

namespace BackendApp.Interfaces;

public interface IChannelRepository {
  bool DoesChannelExistForUser(int channelId, int userId);
  List<Message> GetMessagesByChannelId(int channelId);

  List<User> GetUsersByChannelId(int channelId);

  string GetChannelNameById(int channelId);

  bool IsUserAdmin(int channelId, int userId);

  void AddUserToChannel(int channelId, int userId);

  void UpdateChannelName(int channelId, string newName);
}