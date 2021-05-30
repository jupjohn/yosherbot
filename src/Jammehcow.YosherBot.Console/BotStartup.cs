using System;
using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Jammehcow.YosherBot.Common.Configurations;
using Jammehcow.YosherBot.Common.Helpers.Environment;
using Jammehcow.YosherBot.Console.Helpers.Logger;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.Console
{
    public partial class BotStartup : BackgroundService
    {
        private readonly DiscordSocketClient _client;
        private readonly GeneralOptions _options;
        private readonly CommandService _commandService;
        private readonly IServiceProvider _serviceProvider;
        private readonly IHostApplicationLifetime _applicationLifetime;

        private readonly IDiscordLogger _discordLogger;
        private readonly ILogger<BotStartup> _logger;

        public BotStartup(IDiscordLogger genericDiscordDiscordLogger, ILogger<BotStartup> logger,
            CommandService commandService, IServiceProvider serviceProvider,
            IHostApplicationLifetime applicationLifetime, DiscordSocketClient client, GeneralOptions options)
        {
            _discordLogger = genericDiscordDiscordLogger;
            _logger = logger;
            _commandService = commandService;
            _serviceProvider = serviceProvider;
            _applicationLifetime = applicationLifetime;
            _client = client;
            _options = options;
        }

        // TODO: move to handler class
        private async Task HandleOnMessageReceivedAsync(SocketMessage socketMessage)
        {
            // ReSharper disable once UseNegatedPatternMatching
            var message = socketMessage as SocketUserMessage;
            if (message == null)
                return;

            var argPos = 0;
            var prefix = _options.Prefix;
            if (!message.HasStringPrefix(prefix, ref argPos) || message.Author.IsBot)
                return;

            // TODO: move this to DB lookup (can cache at startup)
            if (EnvironmentsHelper.IsProduction() && socketMessage.Channel.Name != "commands-use")
                return;

            _logger.LogInformation("Attempting to handle command for message {Message}", message.Content);

            var context = new SocketCommandContext(_client, message);
            await _commandService.ExecuteAsync(context, argPos, _serviceProvider);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var cancellationSource = await InitialiseClientAsync(stoppingToken);

            await _client.LoginAsync(TokenType.Bot, GetBotToken());
            await _client.StartAsync();

            try
            {
                await Task.Delay(Timeout.Infinite, cancellationSource.Token);
                _logger.LogInformation("Stopping requested");
            }
            finally
            {
                // There's a chance this has already been stopped internally so don't re-call it (might be noop anyways)
                if (_client.ConnectionState == ConnectionState.Connected)
                    await _client.StopAsync();

                // Dispose the client, even though .NET Generic Host should be able to do that.
                // Be safe :)
                _client.Dispose();

                // Call a full host stop to prevent the bot being stopped but the host spinning
                _applicationLifetime.StopApplication();
            }
        }
    }
}
