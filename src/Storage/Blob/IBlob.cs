using RicardoGaefke.Domain;
using Azure.Storage.Blobs.Models;

namespace RicardoGaefke.Storage
{
  public interface IBlob
  {
    public void SaveBase64(Image data);
    public void SaveJson(Image data);
    public BlobDownloadInfo Download(string file);
  }
}