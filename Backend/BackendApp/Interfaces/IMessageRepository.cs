namespace BackendApp.Interfaces;

public interface IMessageRepository {
  void addMessage(int userId, int channelId, string text);
}