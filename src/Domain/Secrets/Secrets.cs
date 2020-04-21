namespace RicardoGaefke.Domain
{
  public class Secrets
  {
    public class Login
    {
      public string KeyVault { get; set; }
      public string ClientID { get; set; }
      public string ClientSecret { get; set; }
      public string Blob { get; set; }
    }

    public class ConnectionStrings
    {
      public string SqlServer { get; set; }
      public string Storage { get; set; }
      public string SendGrid { get; set; }
      public string AzureWebJobsDashboard { get; set; }
      public string AzureWebJobsStorage { get; set; }
    }
  }
}