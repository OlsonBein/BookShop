using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Store.BusinessLogicLayer.Helpers;
using static Store.BusinessLogicLayer.Models.Enums.Entity.Enums;

namespace Store.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .ConfigureLogging(logging => logging.SetMinimumLevel(LogLevel.Error));
                });
    }
}
