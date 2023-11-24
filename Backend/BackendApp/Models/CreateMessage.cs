using BackendApp.Interfaces;

namespace BackendApp.Models;

public class CreateMessage : ICreateMessage {
  public string text { get; set; }
  public int channelId { get; set; }

  public CreateMessage(string text, int channelId) {
    this.text = text;
    this.channelId = channelId;
  }
}