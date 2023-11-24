namespace BackendApp.Interfaces;

public interface ICreateMessage {
  string text { get; set; }
  int channelId { get; set; }
}