using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.Host;
using RicardoGaefke.Data;
using RicardoGaefke.Domain;

namespace RicardoGaefke.WebJob.XML
{
  class Program
  {
    static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
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
      ;
  }
}
