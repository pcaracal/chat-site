using BackendApp.Interfaces;
using BackendApp.Models;

namespace BackendApp.Repositories;

public class MessageRepository : IMessageRepository {
  private readonly ApplicationDbContext _context;

  public MessageRepository(ApplicationDbContext context) {
    _context = context;
  }

  public void addMessage(int userId, int channelId, string text) {
    _context.message.Add(new Message(text, userId, channelId));
    _context.SaveChanges();
  }
}