using System;
using System.Linq;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Discord;
using Discord.WebSocket;
using Jammehcow.YosherBot.Common.Helpers.Environment;
using Jammehcow.YosherBot.Console.Extensions;
using Jammehcow.YosherBot.EfCore;
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
                // TODO: move to service rather than being across all services by default.
                .ConfigureDiscordBot(() => new DiscordSocketConfig
                {
                    LogLevel = EnvironmentsHelper.IsDevelopment() ? LogSeverity.Verbose : LogSeverity.Info,
                    MessageCacheSize = 40,
                    HandlerTimeout = 2000,
                    AlwaysDownloadUsers = true
                })
                .ConfigureLogging(builder =>
                {
                    // TODO: file/APM logging
                    builder.ClearProviders();
                    builder.AddConsole();
                })
                .ConfigureServices(builder =>
                {
                    builder.AddOptions();
                    // TODO: figure out why I can't pool. Overhead of DbContext construction is blocking socket
                    builder.AddDbContext<YosherBotContext>();
                })
                .UseServiceProviderFactory(new AutofacServiceProviderFactory(builder =>
                {
                    var ownedAssemblies = AppDomain.CurrentDomain.GetAssemblies()
                        .Where(a => a.GetName().FullName.Contains(nameof(YosherBot)))
                        .ToArray();
                    builder.RegisterAssemblyModules(ownedAssemblies);
                }));
    }
}
