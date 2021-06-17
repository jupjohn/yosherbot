using Jammehcow.YosherBot.EfCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.Migration
{
    class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.AddConsole();
                })
                .ConfigureServices(builder =>
                {
                    builder.AddOptions();
                    builder.AddEntityFrameworkNpgsql();
                    builder.AddDbContext<YosherBotContext>();
                });
    }
}
