using RicardoGaefke.Domain;
using Azure.Storage.Blobs.Models;

namespace RicardoGaefke.Storage
{
  public interface IQueue
  {
    public void SaveMessage(string queue, string message);
  }
}