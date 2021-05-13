using Discord;
using Discord.WebSocket;
using Jammehcow.YosherBot.Console.Extensions;
using Jammehcow.YosherBot.Console.Helpers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.Console
{
    public static class Program
    {
        public static void Main(string[] args) => CreateHostBuilder(args).Build().Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureDiscordBot(() => new DiscordSocketConfig
                {
                    LogLevel = EnvironmentsHelper.IsDevelopment() ? LogSeverity.Verbose : LogSeverity.Info,
                    MessageCacheSize = 40,
                    HandlerTimeout = 2000,
                    AlwaysDownloadUsers = true
                })
                .ConfigureLogging(builder =>
                {
                    builder.ClearProviders();
                    builder.AddConsole();
                });
    }
}