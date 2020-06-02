using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RicardoGaefke.Domain;
using RicardoGaefke.Data;
using RicardoGaefke.Storage;

namespace RicardoGaefke.Web.Site
{
    [Route("api/")]
    [ApiController]
  public class FileController : ControllerBase
  {
    private readonly ILogger<FileController> _logger;
    private readonly IMyFiles _myFiles;
    private readonly IBlob _blob;
    private readonly IQueue _queue;

    public FileController(ILogger<FileController> logger, IMyFiles MyFiles, IBlob Blob, IQueue Queue)
    {
        _logger = logger;
        _myFiles = MyFiles;
        _blob = Blob;
        _queue = Queue;
    }

    [HttpPost("SendXML")]
    public ActionResult<BasicReturn> ReceiveXmlFile(Form data)
    {
      BasicReturn _return = new BasicReturn();

      try
      {
        Form myForm = new Form(data.Name, data.Email, data.Fail, data.FileName, data.FileSize, data.FileType);

        Inserted inserted = _myFiles.Insert(myForm);

        Image myFile = new Image(data.FileBase64, data.FileType, $"{inserted.GUID}.xml");

        _blob.SaveBase64(myFile);

        _queue.SaveMessage("webjob-xml", inserted.ID.ToString());

        _return.Success = true;
      }
      catch (DomainException ex)
      {
        _return.Success = false;
        _return.Message = ex.Message;
        _return.Code = "Domain";
      }
      catch (System.Exception ex)
      {
        _return.Success = false;
        _return.Message = ex.Message;
        _return.Details = ex.StackTrace;
      }

      return _return;
    }
  }
}
