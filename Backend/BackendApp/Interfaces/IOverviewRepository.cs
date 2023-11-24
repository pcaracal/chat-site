namespace BackendApp.Interfaces;

public interface IOverviewRepository {
  List<IChannel> GetChannelsByUserId(int userId);
  void createChannel(string name, int userId);
  bool doesChannelExist(string name);
}