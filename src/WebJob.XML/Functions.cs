using System;
using System.Xml;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RicardoGaefke.Data;
using RicardoGaefke.Domain;
using RicardoGaefke.Storage;

namespace RicardoGaefke.WebJob.XML
{
  public class Functions
  {
    private readonly IOptions<Secrets.ConnectionStrings> _connStr;
    private readonly IMyFiles _myFiles;
    private readonly IBlob _blob;

    public Functions(
      IOptions<Secrets.ConnectionStrings> ConnectionStrings,
      IMyFiles MyFiles,
      IBlob Blob
    )
    {
      _connStr = ConnectionStrings;
      _myFiles = MyFiles;
      _blob = Blob;
    }

    public void ProcessQueueMessageWebJobXml
    (
      [QueueTrigger("webjob-xml")]
      string message,
      int DequeueCount,
      ILogger logger
    )
    {
      logger.LogInformation(DequeueCount.ToString());

      Inserted myFiles = _myFiles.GetFileInfo(Convert.ToInt32(message));

      string fileName = $"{myFiles.GUID}.xml";

      XmlDocument doc = new XmlDocument();
      doc.Load(_blob.Download(fileName).Content);

      string json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);

      Image file = new Image(fileName.Replace("xml", "json"), json);

      _blob.SaveJson(file);
    }
  }
}
