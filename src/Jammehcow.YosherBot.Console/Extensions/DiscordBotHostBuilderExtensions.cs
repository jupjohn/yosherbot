using System;
using Discord.Commands;
using Discord.WebSocket;
using Jammehcow.YosherBot.Console.Helpers.Logger;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Jammehcow.YosherBot.Console.Extensions
{
    public static class DiscordBotHostBuilderExtensions
    {
        /// <summary>
        /// Configure a basic discord bot using a hosted (background) service and command modules
        /// </summary>
        /// <param name="builder">The builder to register the client and services with</param>
        /// <param name="configSupplier">A method which returns the Discord client's config</param>
        /// <returns>The IHostBuilder</returns>
        public static IHostBuilder ConfigureDiscordBot(this IHostBuilder builder,
            Func<DiscordSocketConfig> configSupplier)
        {
            builder.ConfigureServices(services =>
            {
                var clientConfig = configSupplier.Invoke();
                if (clientConfig == null)
                    throw new InvalidOperationException("Cannot instantiate a bot with no client configuration!");

                services.AddSingleton<DiscordSocketClient>(new DiscordSocketClient(clientConfig));
                services.AddSingleton<CommandService>();
                services.AddScoped<IDiscordLogger, GenericDiscordLogger>();
                services.AddHostedService<BotStartup>();
            });

            return builder;
        }
    }
}