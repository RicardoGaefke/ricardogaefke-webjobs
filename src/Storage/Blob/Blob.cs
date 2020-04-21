using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System;
using System.IO;
using Microsoft.Extensions.Options;
using RicardoGaefke.Domain;

namespace RicardoGaefke.Storage
{
  public class Blob : IBlob
  {
    private readonly IOptions<Secrets.ConnectionStrings> _connStr;

    public Blob(IOptions<Secrets.ConnectionStrings> ConnectionStrings)
    {
      _connStr = ConnectionStrings;
    }
    public void SaveBase64(Image data)
    {
      CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_connStr.Value.Storage);
      CloudBlobClient cloudBlobClient = cloudStorageAccount.CreateCloudBlobClient();
      CloudBlobContainer container = cloudBlobClient.GetContainerReference("webjob-xml");
      CloudBlockBlob blob = container.GetBlockBlobReference(data.Name);

      byte[] img = Convert.FromBase64String(data.Src.Substring(data.Src.IndexOf(",") + 1));
      blob.Properties.ContentType = data.Mime;
      blob.UploadFromByteArray(img, 0, img.Length);
    }

    public BlobDownloadInfo Download(string file)
    {
      BlobServiceClient blobServiceClient = new BlobServiceClient(_connStr.Value.Storage);
      BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient("webjob-xml");
      BlobClient blobClient = containerClient.GetBlobClient(file);

      return blobClient.Download();
    }
  }
}