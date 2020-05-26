using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using RicardoGaefke.Domain;
using RicardoGaefke.Storage;

namespace RicardoGaefke.Email
{
  public class MyEmail : IMyEmail
  {
    private readonly IOptions<Secrets.ConnectionStrings> _connectionStrings;
    private readonly IBlob _blob;

    public MyEmail(IOptions<Secrets.ConnectionStrings> ConnectionStrings,
      IBlob Blob)
    {
      _connectionStrings = ConnectionStrings;
      _blob = Blob;
    }

    public async Task<string> SendSuccessMessage(Form data)
    {
      SendGridClient client = new SendGridClient(_connectionStrings.Value.SendGrid);

      var msg = new SendGridMessage()
      {
        From = new EmailAddress("donotreply@ricardogaefke.com", "Ricardo Gaefke"),
        Subject = "Success! Your XML to JSON converter report",
        PlainTextContent = "Success! Your xml file was converted by Ricardo Gaefke's WebJob.",
        HtmlContent = "<strong>Success! Your xml file was converted by Ricardo Gaefke's WebJob.</strong>"
      };

      msg.AddTo(new EmailAddress(data.Email, data.Name));

      if (data.Email != "ricardogaefke@gmail.com")
      {
        msg.AddBcc(new EmailAddress("ricardogaefke@gmail.com", "Ricardo Gaefke"));
      }

      MemoryStream ms = new MemoryStream();
      _blob.Download(data.FileName).Content.CopyTo(ms);

      msg.AddAttachment(data.FileName, Convert.ToBase64String(ms.ToArray()));

      Response response = await client.SendEmailAsync(msg);

      return response.Headers.GetValues("x-message-id").FirstOrDefault();
    }
  }
}
