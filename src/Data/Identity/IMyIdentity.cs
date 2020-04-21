using RicardoGaefke.Domain;

namespace RicardoGaefke.Data
{
  public interface IMyIdentity
  {
    public User SignIn(string email, string password);
    public BasicReturn ValidateLastChanged(string user, string lastChanged, string url);

    public void SignOut(int UserID);

    public void Record(int UserID, int Status, string Details);

    public void ChangeName(int UserID, string Name, string Password, string Url);
    public void ChangeEmail(int UserID, string Email, string Password, string Url);
    public void ChangePassword(int UserID, string NewPassword, string Password, string Url);
  }
}