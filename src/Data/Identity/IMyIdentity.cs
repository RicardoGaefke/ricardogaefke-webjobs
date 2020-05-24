using RicardoGaefke.Domain;

namespace RicardoGaefke.Data
{
  public interface IMyIdentity
  {
    User SignIn(string email, string password);
    BasicReturn ValidateLastChanged(string user, string lastChanged, string url);

    void SignOut(int UserID);

    void Record(int UserID, int Status, string Details);

    void ChangeName(int UserID, string Name, string Password, string Url);
    void ChangeEmail(int UserID, string Email, string Password, string Url);
    void ChangePassword(int UserID, string NewPassword, string Password, string Url);
  }
}