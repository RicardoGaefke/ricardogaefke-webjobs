using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RicardoGaefke.Data;
using RicardoGaefke.Domain;
using RicardoGaefke.Email;
using RicardoGaefke.Storage;

namespace RicardoGaefke.WebJob.XML
{
  class Program
  {
    static async Task Main(string[] args)
    {
      var host = CreateHostBuilder(args).Build();

      using (host)
      {
        await host.RunAsync();
      }
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureHostConfiguration(configHost =>
        {
            configHost.SetBasePath(Directory.GetCurrentDirectory());
            configHost.AddJsonFile("appsettings.json", optional: true);
            configHost.AddCommandLine(args);
        })
        .ConfigureServices((context, services) =>
        {
          var Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build()
          ;

          services.Configure<Secrets.ConnectionStrings>(Configuration.GetSection("ConnectionStrings"));
          services.AddSingleton<IMyFiles, MyFiles>();
          services.AddSingleton<IBlob, Blob>();
          services.AddSingleton<IQueue, Queue>();
          services.AddSingleton<IMyEmail, MyEmail>();
        })
        .ConfigureLogging((context, b) =>
        {
            b.AddConsole();
        })
        .ConfigureWebJobs(b =>
        {
            b.AddAzureStorageCoreServices();
            b.AddAzureStorage();
        })
        .UseConsoleLifetime()
      ;
  }
}
