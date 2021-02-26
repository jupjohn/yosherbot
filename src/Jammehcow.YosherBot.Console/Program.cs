using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Jammehcow.YosherBot.Console.Helpers.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.Console
{
    public static class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.AddConsole();
                })
                .ConfigureServices(services =>
                {
                    var sockConfig = new DiscordSocketConfig
                    {
                        LogLevel = LogSeverity.Verbose,
                        MessageCacheSize = 40,
                        HandlerTimeout = 2000
                    };
                    services.AddSingleton<DiscordSocketClient>(new DiscordSocketClient(sockConfig));
                    services.AddSingleton<CommandService>();
                    services.AddScoped<IDiscordLogger, GenericDiscordLogger>();
                    services.AddHostedService<BotStartup>();
                });
    }
}