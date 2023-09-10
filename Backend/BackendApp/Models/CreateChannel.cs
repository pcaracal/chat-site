using BackendApp.Interfaces;

namespace BackendApp.Models;

public class CreateChannel : ICreateChannel {
  public string name { get; set; }

  public CreateChannel(string name) {
    this.name = name;
  }
}