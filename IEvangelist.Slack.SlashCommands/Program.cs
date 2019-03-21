using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.AzureAppServices;

namespace IEvangelist.Slack.SlashCommands
{
    public class Program
    {
        public static void Main(string[] args) 
            => CreateWebHostBuilder(args).Build().Run();

        static IWebHostBuilder CreateWebHostBuilder(string[] args)
            => WebHost.CreateDefaultBuilder(args)
                      .ConfigureLogging(logging => logging.AddAzureWebAppDiagnostics())
                      .ConfigureServices(services =>
                                         services.Configure<AzureFileLoggerOptions>(
                                                  options =>
                                                  {
                                                      options.FileName = "azure-diagnostics-";
                                                      options.FileSizeLimit = 50 * 1024;
                                                      options.RetainedFileCountLimit = 5;
                                                  })
                                                 .Configure<AzureBlobLoggerOptions>(
                                                 options => options.BlobName = "log.txt"))
                      .UseStartup<Startup>();
    }
}