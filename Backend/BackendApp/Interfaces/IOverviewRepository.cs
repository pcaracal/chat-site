namespace BackendApp.Interfaces;

public interface IOverviewRepository {
  List<IChannel> GetChannelsByUserId(int userId);
}