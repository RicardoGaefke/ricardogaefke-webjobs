using System;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Queue;
using Microsoft.Extensions.Options;
using RicardoGaefke.Domain;

namespace RicardoGaefke.Storage
{
  public class Queue : IQueue
  {
    private readonly IOptions<Secrets.ConnectionStrings> _connStr;

    public Queue(IOptions<Secrets.ConnectionStrings> ConnectionStrings)
    {
      _connStr = ConnectionStrings;
    }

    private CloudQueue MyQueue(string queue)
    {
      CloudStorageAccount cloudStorageAccount = CloudStorageAccount.Parse(_connStr.Value.Storage);
      CloudQueueClient queueClient = cloudStorageAccount.CreateCloudQueueClient();
      CloudQueue myQueue = queueClient.GetQueueReference(queue);
      myQueue.CreateIfNotExists();
      return myQueue;
    }

    public void SaveMessage(string queue, string message)
    {
      CloudQueue myQueue = MyQueue(queue);
      myQueue.AddMessage(new CloudQueueMessage(message));
    }
  }
}
