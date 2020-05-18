using System;
namespace RicardoGaefke.Domain
{
  public class Inserted
  {
    public int ID {get;set;}
    public string GUID { get; set; }

    public Inserted(int id, string guid)
    {
      DomainException.When(!string.IsNullOrEmpty(guid), "GUID is required!");

      this.ID = id;
      this.GUID = guid;
    }
  }
}
