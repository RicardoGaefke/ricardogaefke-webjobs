using Microsoft.Azure.Storage.Queue;

namespace RicardoGaefke.Storage
{
  public interface IQueue
  {
    public void SaveMessage(string queue, string message);
  }
}
