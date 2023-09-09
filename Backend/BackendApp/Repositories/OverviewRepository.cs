using BackendApp.Interfaces;
using BackendApp.Models;

namespace BackendApp.Repositories;

public class OverviewRepository : IOverviewRepository {
  private readonly ApplicationDbContext _context;

  public OverviewRepository(ApplicationDbContext context) {
    _context = context;
  }

  public List<IChannel> GetChannelsByUserId(int userId) {
    List<IUserChannel> userChannels =
      _context.user_channel.Where(uc => uc.fk_user_id == userId).ToList<IUserChannel>();

    List<IChannel> channels = new List<IChannel>();
    foreach (IUserChannel uc in userChannels) {
      var channel = _context.channel.FirstOrDefault(c => c.id == uc.fk_channel_id);
      if (channel != null) channels.Add(_context.channel.First(c => c.id == uc.fk_channel_id));
    }

    return channels;
  }

  public bool doesChannelExist(string name) {
    return _context.channel.Any(c => c.name == name);
  }

  public void createChannel(string name, int userId) {
    Channel channel = new Channel(name, userId);
    _context.channel.Add(channel);
    _context.SaveChanges();
    int channelId = _context.channel.First(c => c.name == name).id;
    _context.user_channel.Add(new UserChannel(userId, channelId));
    _context.SaveChanges();
  }
}