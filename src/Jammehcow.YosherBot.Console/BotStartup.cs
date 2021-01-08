using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Jammehcow.YosherBot.Console.Extensions;
using Jammehcow.YosherBot.Console.Helpers;
using Jammehcow.YosherBot.Console.Helpers.Logger;

namespace Jammehcow.YosherBot.Console
{
    public class BotStartup
    {
        private DiscordSocketClient _client;
        private readonly string _botToken;
        private readonly DiscordSocketConfig _sockConfig;

        private CommandService _commandService;
        private IServiceProvider _serviceProvider;

        private readonly IDiscordLogger _logger;

        public static BotStartup CreateDefaultBot()
        {
            static void ThrowOnMissingToken() => throw new ArgumentException("No bot token was provided");

            var discordBotToken = EnvironmentsHelper.GetDiscordBotToken()
                .IfExists(value =>
                {
                    // Throw if token is empty, not just null
                    if (string.IsNullOrWhiteSpace(value)) ThrowOnMissingToken();
                })
                .IfMissing(ThrowOnMissingToken);

            return new BotStartup(discordBotToken.It);
        }

        private BotStartup(string botToken)
        {
            _botToken = botToken;
            // TODO: inject
            _logger = new GenericDiscordLogger();
            _sockConfig = new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
                MessageCacheSize = 40,
                HandlerTimeout = 2000
            };

            InitialiseClient();
        }

        private void InitialiseClient()
        {
            _client = new DiscordSocketClient(_sockConfig);

            _commandService = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Info,
                DefaultRunMode = RunMode.Async,
                IgnoreExtraArgs = true,
                CaseSensitiveCommands = false
            });

            _client.Log += _logger.HandleLogEventAsync;
            _client.MessageReceived += HandleOnMessageReceivedAsync;
        }

        public async Task Run()
        {
            await _client.LoginAsync(TokenType.Bot, _botToken);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private async Task HandleOnMessageReceivedAsync(SocketMessage socketMessage)
        {
            // Bail out if it's a System Message.
            if (!(socketMessage is SocketUserMessage msg)) return;

            // We don't want the bot to respond to itself or other bots.
            if (msg.Author.Id == _client.CurrentUser.Id || msg.Author.IsBot) return;

            if (string.Equals(msg.Content, "$ping"))
                await msg.Channel.SendMessageAsync("Pong!");

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
    }
}