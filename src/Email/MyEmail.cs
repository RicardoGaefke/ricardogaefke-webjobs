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

    private SendGridMessage message(string subject, string plainTextContent, string htmlContent)
    {
      return new SendGridMessage()
      {
        From = new EmailAddress("donotreply@ricardogaefke.com", "Ricardo Gaefke"),
        Subject = subject,
        PlainTextContent = plainTextContent,
        HtmlContent = htmlContent
      };
    }

    public async Task<string> SendSuccessMessage(Form data)
    {
      SendGridClient client = new SendGridClient(_connectionStrings.Value.SendGrid);

      var msg = message(
        "Success! Your XML to JSON converter report",
        "Success! Your xml file was converted by Ricardo Gaefke's WebJob.",
        "<strong>Success! Your xml file was converted by <a href=\"http://webjobs.ricardogaefke.com\">Ricardo Gaefke's WebJob</a>.</strong>"
      );

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

    public async Task<string> SendErrorMessage(Form data)
    {
      SendGridClient client = new SendGridClient(_connectionStrings.Value.SendGrid);

      var msg = message(
        "Failed! Your XML to JSON converter report",
        "Failed! Your xml file was NOT converted by Ricardo Gaefke's WebJob.",
        $"<strong>Failed! Your xml file was NOT converted by <a href=\"http://webjobs.ricardogaefke.com\">Ricardo Gaefke's WebJob</a>.</strong>"
      );

      msg.AddTo(new EmailAddress(data.Email, data.Name));

      if (data.Email != "ricardogaefke@gmail.com")
      {
        msg.AddBcc(new EmailAddress("ricardogaefke@gmail.com", "Ricardo Gaefke"));
      }

      Response response = await client.SendEmailAsync(msg);

      return response.Headers.GetValues("x-message-id").FirstOrDefault();
    }
  }
}
