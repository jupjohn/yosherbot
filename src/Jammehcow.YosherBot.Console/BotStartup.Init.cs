using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Jammehcow.YosherBot.Console.Extensions;
using Jammehcow.YosherBot.Console.Helpers;

namespace Jammehcow.YosherBot.Console
{
    public partial class BotStartup
    {
        private async Task<CancellationTokenSource> InitialiseClientAsync(CancellationToken? externalCancellationToken)
        {
            await _commandService.AddModulesAsync(Assembly.GetAssembly(typeof(Command.ColorMe.ColorMeHandlerService)),
                _serviceProvider);

            _client.Log += _logger.HandleLogEventAsync;
            _client.MessageReceived += HandleOnMessageReceivedAsync;

            var cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(
                externalCancellationToken ?? CancellationToken.None);

            // ReSharper disable once MethodSupportsCancellation
            _client.Disconnected += _ =>
            {
                // TODO: handle and log exceptions. This is only meant for managed quit events
                if (!cancellationSource.IsCancellationRequested)
                    cancellationSource.Cancel();

                return Task.CompletedTask;
            };

            return cancellationSource;
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
