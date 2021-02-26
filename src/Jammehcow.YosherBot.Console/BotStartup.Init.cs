using System;
using System.Reflection;
using System.Threading.Tasks;
using Jammehcow.YosherBot.Console.Extensions;
using Jammehcow.YosherBot.Console.Helpers;

namespace Jammehcow.YosherBot.Console
{
    public partial class BotStartup
    {
        private async Task InitialiseClientAsync()
        {
            await _commandService.AddModulesAsync(Assembly.GetAssembly(typeof(Command.ColorMe.ColorMeHandlerService)),
                _serviceProvider);

            _client.Log += _logger.HandleLogEventAsync;
            _client.MessageReceived += HandleOnMessageReceivedAsync;
        }

        internal static string GetBotToken()
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