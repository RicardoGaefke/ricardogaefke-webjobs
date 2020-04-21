using System.Threading.Tasks;

namespace RicardoGaefke.Email
{
  public interface IMyEmail
  {
    Task<string> SendMessage();
  }
}