using System;
using System.IO;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RicardoGaefke.Data;
using RicardoGaefke.Domain;

namespace RicardoGaefke.WebJob.XML
{
  public class Functions
  {
    private readonly IOptions<Secrets.ConnectionStrings> _connStr;

    public Functions(
      IOptions<Secrets.ConnectionStrings> ConnectionStrings
    )
    {
      _connStr = ConnectionStrings;
    }

    public void ProcessXMLFile(
      [QueueTrigger("xml")] string message,
      [Blob("xml/{queueTrigger}", FileAccess.Read)] Stream myBlob,
      ILogger logger
    )
    {
      logger.LogInformation($"Blob name:{message} \n Size: {myBlob.Length} bytes");
    }

    public void ProcessQueueMessage
    (
      [QueueTrigger("xml")]
      string message,
      ILogger logger
    )
    {
      logger.LogInformation(message);
    }
  }
}
