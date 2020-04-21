using System;
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

    public async Task<string> SendMessage()
    {
      SendGridClient client = new SendGridClient(_connectionStrings.Value.SendGrid);

      var msg = new SendGridMessage()
      {
        From = new EmailAddress("donotreply@ricardogaefke.com", "Ricardo Gaefke"),
        Subject = "Ricardo Gaefke - processamento de XML",
        HtmlContent = "<strong>Enviado pelo sistema with C# and DI</strong>"
      };

      msg.AddCc(new EmailAddress("ricardogaefke@gmail.com", "Ricardo Gaefke"));
      msg.AddTo(new EmailAddress("lucasneves.dev@gmail.com", "Lucas Neves"));

      Response response = await client.SendEmailAsync(msg);

      return response.Headers.GetValues("x-message-id").FirstOrDefault();
    }
  }
}