using Hangfire.Console.Extensions.Serilog;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace AutoGit.Bot
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog((context, provider, configuration) =>
                {
                    configuration.ReadFrom.Configuration(context.Configuration);
                    configuration.Enrich.WithHangfireContext();
                    configuration.WriteTo.Hangfire();
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }
    }
}