namespace RicardoGaefke.Domain
{
  public class Image : BasicReturn
  {
    public int Id {get;set;}
    public string Src {get;set;}
    public string Name {get;set;}
    public string Alt_PT { get; set; }
    public string Alt_ENG {get;set;}

    public string Data {get;set;}
    public string Mime {get;set;}
    public int Height {get;set;}
    public int Width {get;set;}
    public int Size {get;set;}
    public string CreatedBy { get; set; }
    public string Created { get; set; }

    public Image()
    {
    }

    public Image (string data, string mime, string name)
    {
      DomainException.When(!string.IsNullOrEmpty(data), "Data is required!");
      DomainException.When(!string.IsNullOrEmpty(mime), "Mime is required!");
      DomainException.When(!string.IsNullOrEmpty(name), "Name is required!");

      this.Data = data;
      this.Mime = mime;
      this.Name = name;
    }

    public Image(string name, string src)
    {
      DomainException.When(!string.IsNullOrEmpty(name), "Data is required!");
      DomainException.When(!string.IsNullOrEmpty(src), "SRC is required!");

      this.Name = name;
      this.Src = src;
    }

    public Image(string data, string mime, string user, string alt_PT, string alt_ENG)
    {
      DomainException.When(!string.IsNullOrEmpty(data), "Data is required!");
      DomainException.When(!string.IsNullOrEmpty(mime), "Mime is required!");

      DomainException.When(!string.IsNullOrEmpty(alt_PT), "Alt_PT is required!");
      DomainException.When(!(alt_PT.Length < 5), "Alt_PT - 5 character minimum!");
      DomainException.When(!(alt_PT.Length > 95), "Alt_PT - 95 character maximum!");

      DomainException.When(!string.IsNullOrEmpty(alt_ENG), "Alt_ENG is required!");
      DomainException.When(!(alt_ENG.Length < 5), "Alt_ENG - 5 character minimum!");
      DomainException.When(!(alt_ENG.Length > 95), "Alt_ENG - 95 character maximum!");

      DomainException.When(!string.IsNullOrEmpty(user), "CreatedBy is required!");

      this.Data = data;
      this.Mime = mime;
      this.Alt_PT = alt_PT;
      this.Alt_ENG = alt_ENG;
      this.CreatedBy = user;
    }
  }
}
