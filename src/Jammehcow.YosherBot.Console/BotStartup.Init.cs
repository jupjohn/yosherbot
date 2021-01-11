using System;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Jammehcow.YosherBot.Console.Extensions;
using Jammehcow.YosherBot.Console.Helpers;

namespace Jammehcow.YosherBot.Console
{
    public partial class BotStartup
    {
        private void InitialiseClient()
        {
            var sockConfig = new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Info,
                MessageCacheSize = 40,
                HandlerTimeout = 2000
            };
            _client = new DiscordSocketClient(sockConfig);

            _client.Log += _logger.HandleLogEventAsync;
            _client.MessageReceived += HandleOnMessageReceivedAsync;
        }

        private void InitialiseCommands()
        {
            _commandService = new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Info,
                DefaultRunMode = RunMode.Async,
                IgnoreExtraArgs = true,
                CaseSensitiveCommands = false
            });
        }

        private static string GetBotToken()
        {
            static void ThrowOnMissingToken() => throw new ArgumentException("No bot token was provided");

            var discordBotToken = EnvironmentsHelper.GetDiscordBotToken()
                .IfExists(value =>
                {
                    // Throw if token is empty, not just null
                    if (string.IsNullOrWhiteSpace(value)) ThrowOnMissingToken();
                })
                .IfMissing(ThrowOnMissingToken);

            return discordBotToken.It;
        }
    }
}