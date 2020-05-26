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
    private readonly IQueue _queue;
    private readonly IMyEmail _myEmail;

    public Functions(
      IOptions<Secrets.ConnectionStrings> ConnectionStrings,
      IMyFiles MyFiles,
      IBlob Blob,
      IMyEmail MyEmail,
      IQueue Queue
    )
    {
      _connStr = ConnectionStrings;
      _myFiles = MyFiles;
      _blob = Blob;
      _myEmail = MyEmail;
      _queue = Queue;
    }

    public async void ProcessQueueMessageWebJobXml
    (
      [QueueTrigger("webjob-xml")]
      string message,
      int DequeueCount,
      ILogger logger
    )
    {
      try
      {
        Inserted myFiles = _myFiles.GetFileInfo(Convert.ToInt32(message));

        if (myFiles.Fail)
        {
          _queue.SaveMessage("webjob-xml-poison", message);
          return;
        }

        string fileName = $"{myFiles.GUID}.xml", fileNameJson = $"{myFiles.GUID}.json";

        XmlDocument doc = new XmlDocument();
        doc.Load(_blob.Download(fileName).Content);

        string json = JsonConvert.SerializeXmlNode(doc, Newtonsoft.Json.Formatting.Indented);

        Image file = new Image(fileNameJson, json);

        _blob.SaveJson(file);

        Form mailMsg = new Form(myFiles.Name, myFiles.Email, fileNameJson);

        string sgID = await _myEmail.SendSuccessMessage(mailMsg);

        Inserted update = new Inserted(Convert.ToInt32(message), true, DequeueCount, sgID);

        _myFiles.UpdateFileInfo(update);
      }
      catch (System.Exception)
      {
        _queue.SaveMessage("webjob-xml-poison", message);
      }
    }

    public async void ProcessQueueMessageWebJobXmlPoison
    (
      [QueueTrigger("webjob-xml-poison")]
      string message,
      int DequeueCount,
      ILogger logger
    )
    {
      Inserted myFiles = _myFiles.GetFileInfo(Convert.ToInt32(message));

      Form mailMsg = new Form(myFiles.Name, myFiles.Email);

      string sgID = await _myEmail.SendErrorMessage(mailMsg);

      Inserted update = new Inserted(Convert.ToInt32(message), false, DequeueCount, sgID);

      _myFiles.UpdateFileInfo(update);
    }
  }
}
