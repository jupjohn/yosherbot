using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Jammehcow.YosherBot.Console.Extensions;
using Jammehcow.YosherBot.Console.Helpers;

namespace Jammehcow.YosherBot.Console
{
    public class BotStartup
    {
        private DiscordSocketClient _client;
        private readonly string _botToken;

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
        }

        public async Task Run()
        {
            _client = new DiscordSocketClient();
            _client.Log += GenericLog;

            await _client.LoginAsync(TokenType.Bot, _botToken);
            await _client.StartAsync();

            await Task.Delay(-1);
        }

        private static Task GenericLog(LogMessage message)
        {
            System.Console.WriteLine(message.Message);
            return Task.CompletedTask;
        }
    }
}