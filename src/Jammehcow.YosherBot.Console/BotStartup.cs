using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Jammehcow.YosherBot.Common.Configurations;
using Jammehcow.YosherBot.Console.Helpers.Logger;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Jammehcow.YosherBot.Console
{
    public partial class BotStartup : BackgroundService
    {
        private readonly DiscordSocketClient _client;
        private readonly GeneralConfiguration _configuration;
        private readonly CommandService _commandService;
        private readonly IServiceProvider _serviceProvider;

        private readonly IDiscordLogger _logger;

        public BotStartup(IDiscordLogger genericDiscordLogger, CommandService commandService,
            IServiceProvider serviceProvider, DiscordSocketClient client, GeneralConfiguration configuration)
        {
            _logger = genericDiscordLogger;
            _commandService = commandService;
            _serviceProvider = serviceProvider;
            _client = client;
            _configuration = configuration;
        }

        // TODO: move to handler class
        private async Task HandleOnMessageReceivedAsync(SocketMessage socketMessage)
        {
            // ReSharper disable once UseNegatedPatternMatching
            var message = socketMessage as SocketUserMessage;
            if (message == null)
                return;

            var argPos = 0;
            var prefix = _configuration.Prefix;
            if (!message.HasStringPrefix(prefix, ref argPos) || message.Author.IsBot)
                return;

            var context = new SocketCommandContext(_client, message);

            await _commandService.ExecuteAsync(context, argPos, _serviceProvider);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await InitialiseClientAsync();
            await _client.LoginAsync(TokenType.Bot, GetBotToken());
            await _client.StartAsync();

            try
            {
                await Task.Delay(Timeout.Infinite, stoppingToken);
            }
            finally
            {
                // This doesn't hit when using debugger
                await _client.StopAsync();
            }
        }
    }
}
