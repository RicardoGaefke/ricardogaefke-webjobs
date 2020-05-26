using System.Threading.Tasks;
using RicardoGaefke.Domain;

namespace RicardoGaefke.Email
{
  public interface IMyEmail
  {
    Task<string> SendSuccessMessage(Form data);
    Task<string> SendErrorMessage(Form data);
  }
}
