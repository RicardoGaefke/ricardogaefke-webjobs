using System;
using System.Xml;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RicardoGaefke.Data;
using RicardoGaefke.Domain;
using RicardoGaefke.Email;
using RicardoGaefke.Storage;

namespace RicardoGaefke.WebJob.XML
{
  public class Functions
  {
    private readonly IOptions<Secrets.ConnectionStrings> _connStr;
    private readonly IMyFiles _myFiles;
    private readonly IBlob _blob;
    private readonly IMyEmail _myEmail;

    public Functions(
      IOptions<Secrets.ConnectionStrings> ConnectionStrings,
      IMyFiles MyFiles,
      IBlob Blob,
      IMyEmail MyEmail
    )
    {
      _connStr = ConnectionStrings;
      _myFiles = MyFiles;
      _blob = Blob;
      _myEmail = MyEmail;
    }

    public async void ProcessQueueMessageWebJobXml
    (
      [QueueTrigger("webjob-xml")]
      string message,
      int DequeueCount,
      ILogger logger
    )
    {
      logger.LogInformation(DequeueCount.ToString());

      Inserted myFiles = _myFiles.GetFileInfo(Convert.ToInt32(message));

      string fileName = $"{myFiles.GUID}.xml", fileNameJson = $"{myFiles.GUID}.json";

      XmlDocument doc = new XmlDocument();
      doc.Load(_blob.Download(fileName).Content);

      string json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);

      Image file = new Image(fileNameJson, json);

      _blob.SaveJson(file);

      Form mailMsg = new Form(myFiles.Name, myFiles.Email, fileNameJson);

      string sgID = await _myEmail.SendSuccessMessage(mailMsg);

      logger.LogDebug(sgID);
    }
  }
}
