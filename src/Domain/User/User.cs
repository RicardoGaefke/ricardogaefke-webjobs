using System.Collections.Generic;

namespace RicardoGaefke.Domain
{
  public class User : BasicReturn
  {
    public int Id {get; set;}
    public string Name {get; set;}
    public string Email {get; set;}
    public string Password {get; set;}
    public string NewPassword { get; set; }
    public bool KeepConnected {get; set;}
    public bool IsAuthenticated {get; set;}
    public string LastChanged {get; set;}
    public List<string> Roles {get; set;}

    public User(string email, string password, bool keepConnected)
    {
      DomainException.When(!string.IsNullOrEmpty(email), "Email is required!");
      DomainException.When(!string.IsNullOrEmpty(password), "Password is required!");

      this.Email = email;
      this.Password = password;
      this.KeepConnected = keepConnected;
    }

    public User(string email, string password)
    {
      DomainException.When(!string.IsNullOrEmpty(email), "Email is required!");
      DomainException.When(!string.IsNullOrEmpty(password), "Password is required!");

      this.Email = email;
      this.Password = password;
    }

    public User()
    {
      this.Success = true;
    }
  }
}