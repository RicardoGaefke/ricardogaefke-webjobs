using System;
namespace RicardoGaefke.Domain
{
  public class Form
  {
    public string Name {get;set;}
    public string Email { get; set; }
    public bool Fail { get; set; }
    public string FileName { get; set; }
    public int FileSize { get; set; }
    public string FileType { get; set; }
    public string FileBase64 { get; set; }

    public Form()
    {}

    public Form(string name, string email)
    {
      DomainException.When(!string.IsNullOrEmpty(name), "Name is required!");
      DomainException.When(!string.IsNullOrEmpty(email), "Email is required!");

      this.Name = name;
      this.Email = email;
    }

    public Form(string name, string email, string fileName)
    {
      DomainException.When(!string.IsNullOrEmpty(name), "Name is required!");
      DomainException.When(!string.IsNullOrEmpty(email), "Email is required!");
      DomainException.When(!string.IsNullOrEmpty(fileName), "FileName is required!");

      this.Name = name;
      this.Email = email;
      this.FileName = fileName;
    }
    public Form(string name, string email, bool fail, string fileName, int fileSize, string fileType)
    {
      DomainException.When(!string.IsNullOrEmpty(name), "Name is required!");
      DomainException.When(!string.IsNullOrEmpty(email), "Email is required!");
      DomainException.When(!string.IsNullOrEmpty(fileName), "FileName is required!");

      this.Name = name;
      this.Email = email;
      this.Fail = fail;
      this.FileName = fileName;
      this.FileType = fileType;
    }
  }
}
