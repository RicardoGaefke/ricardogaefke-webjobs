using System;
namespace RicardoGaefke.Domain
{
  public class Inserted
  {
    public int ID {get;set;}
    public string GUID { get; set; }
    public string Name { get; set; }
        public string Email { get; set; }
        public bool Fail { get; set; }

    public Inserted(int id, string guid)
    {
      DomainException.When(!string.IsNullOrEmpty(guid), "GUID is required!");

      this.ID = id;
      this.GUID = guid;
    }

    public Inserted (int id, string guid, string name, string email, bool fail)
    {
      DomainException.When(!string.IsNullOrEmpty(guid), "GUID is required!");
      DomainException.When(!string.IsNullOrEmpty(name), "Name is required!");
      DomainException.When(!string.IsNullOrEmpty(email), "Email is required!");

      this.ID = id;
      this.GUID = guid;
      this.Name = name;
      this.Email = email;
      this.Fail = fail;
    }
  }
}
