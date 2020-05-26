using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using RicardoGaefke.Domain;

namespace RicardoGaefke.Email
{
  public class MyEmail : IMyEmail
  {
    private readonly IOptions<Secrets.ConnectionStrings> _connectionStrings;

    public MyEmail(IOptions<Secrets.ConnectionStrings> ConnectionStrings)
    {
      _connectionStrings = ConnectionStrings;
    }

    public async Task<string> SendSuccessMessage(Form data)
    {
      SendGridClient client = new SendGridClient(_connectionStrings.Value.SendGrid);

      var msg = new SendGridMessage()
      {
        From = new EmailAddress("donotreply@ricardogaefke.com", "Ricardo Gaefke"),
        Subject = "Success! Your XML to JSON converter report",
        HtmlContent = "<strong>Success! Your xml file was converted.</strong>"
      };

      msg.AddBcc(new EmailAddress("ricardogaefke@gmail.com", "Ricardo Gaefke"));
      msg.AddTo(new EmailAddress(data.Email, data.Name));

      // msg.AddAttachment(data.FileName, _blob.base64);

      Response response = await client.SendEmailAsync(msg);

      return response.Headers.GetValues("x-message-id").FirstOrDefault();
    }
  }
}
