using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RicardoGaefke.Domain;

namespace RicardoGaefke.Web.Site
{
    [Route("api/")]
    [ApiController]
  public class FileController : ControllerBase
  {
    private readonly ILogger<HomeController> _logger;

    public FileController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpPost("SendXML")]
    public ActionResult<BasicReturn> ReceiveXmlFile()
    {
      BasicReturn _return = new BasicReturn();

      try
      {
        _return.Success = true;
      }
      catch (System.Exception ex)
      {
        _return.Success = false;
        _return.Message = ex.Message;
      }

      return _return;
    }
  }
}