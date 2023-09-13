namespace BackendApp.Models;

public class ChannelData {
  public string channelName { get; set; }
  public List<User> users { get; set; }
  public List<Message> messages { get; set; }

  public bool isAdmin { get; set; }

  public ChannelData(string channelName, List<User> users, List<Message> messages, bool isAdmin) {
    this.channelName = channelName;
    this.users = users;
    this.messages = messages;
    this.isAdmin = isAdmin;
  }
}