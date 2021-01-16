using System.Threading;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Jammehcow.YosherBot.Console.Helpers.Logger;
using Microsoft.Extensions.Hosting;

namespace Jammehcow.YosherBot.Console
{
    public partial class BotStartup : IHostedService
    {
        private DiscordSocketClient _client;

        private CommandService _commandService;

        private readonly IDiscordLogger _logger;

        private BotStartup(CommandService commandService)
        {
            // TODO: inject
            _logger = new GenericDiscordLogger();
            _commandService = commandService;

            InitialiseClient();
            InitialiseCommands();
        }

        // TODO: move to handler class
        private async Task HandleOnMessageReceivedAsync(SocketMessage socketMessage)
        {
            // Bail out if it's a System Message.
            if (!(socketMessage is SocketUserMessage msg)) return;

            // We don't want the bot to respond to itself or other bots.
            if (msg.Author.Id == _client.CurrentUser.Id || msg.Author.IsBot) return;

            if (string.Equals(msg.Content, "$ping"))
                await msg.Channel.SendMessageAsync("Pong!");

            // if (string.Equals(msg.Content, "$stop"))
            //     await this();

            // Create a number to track where the prefix ends and the command begins
            // var pos = 0;
            // if (msg.HasCharPrefix('$', ref pos))
            // {
            //     var context = new SocketCommandContext(_client, msg);
            //     var result = await _commandService.ExecuteAsync(context, pos, _serviceProvider);
            //
            //     // TODO: handle result
            // }
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await _client.LoginAsync(TokenType.Bot, GetBotToken());
            await _client.StartAsync();

            await Task.Delay(-1, cancellationToken);
        }

        public async Task StopAsync(CancellationToken _)
        {
            await _client.StopAsync();
        }
    }
}