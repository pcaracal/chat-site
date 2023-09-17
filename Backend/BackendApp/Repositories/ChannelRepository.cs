using BackendApp.Interfaces;
using BackendApp.Models;

namespace BackendApp.Repositories;

public class ChannelRepository : IChannelRepository {
  private readonly ApplicationDbContext _context;

  public ChannelRepository(ApplicationDbContext context) {
    _context = context;
  }


  public bool DoesChannelExistForUser(int channelId, int userId) {
    return _context.user_channel.Any(uc => uc.fk_channel_id == channelId && uc.fk_user_id == userId);
  }

  public List<Message> GetMessagesByChannelId(int channelId) {
    return _context.message.Where(m => m.fk_channel_id == channelId).ToList();
  }

  public List<User> GetUsersByChannelId(int channelId) {
    List<int> userIds = _context.user_channel.Where(uc => uc.fk_channel_id == channelId).Select(uc => uc.fk_user_id)
      .ToList();
    List<User> users = new List<User>();
    userIds.ForEach(_id => {
      Login login = _context.user.First(u => u.id == _id);
      users.Add(new User(login.id, login.username, login.created_at));
    });

    return users;
  }

  public string GetChannelNameById(int channelId) {
    return _context.channel.First(c => c.id == channelId).name;
  }

  public bool IsUserAdmin(int channelId, int userId) {
    return _context.channel.First(c => c.id == channelId).fk_admin_id == userId;
  }

  public void AddUserToChannel(int channelId, int userId) {
    _context.user_channel.Add(new UserChannel(userId, channelId));
    _context.SaveChanges();
  }

  public void UpdateChannelName(int channelId, string newName) {
    Channel channel = _context.channel.First(c => c.id == channelId);
    channel.name = newName;
    _context.SaveChanges();
  }
}