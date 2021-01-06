using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Jammehcow.YosherBot.Console.Helpers;

namespace Jammehcow.YosherBot.Console
{
    public class BotStartup
    {
        private DiscordSocketClient _client;
        private readonly string _botToken;

        public static BotStartup CreateDefaultBot()
        {
            var discordBotToken = EnvironmentsHelper.GetDiscordBotToken()
                .ItOrThrow(new ArgumentException("No bot token was provided"));

            return new BotStartup(discordBotToken);
        }

        private BotStartup(string botToken)
        {
            _botToken = botToken;
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