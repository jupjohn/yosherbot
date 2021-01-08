using System;
using System.Threading.Tasks;
using Discord;

namespace Jammehcow.YosherBot.Console.Helpers.Logger
{
    public class GenericDiscordLogger : IDiscordLogger
    {
        public Task HandleLogEventAsync(LogMessage logMessage)
        {
            // TODO: handle log levels
            var logLevelLiteral = Enum.GetName(typeof(LogSeverity), logMessage.Severity) ?? nameof(LogSeverity.Info);
            var messageParts = EnvironmentsHelper.IsDevelopment()
                ? new[] {$"{logLevelLiteral}\t", logMessage.Source, logMessage.Message}
                : new[] {$"{logLevelLiteral}\t", logMessage.Message};

            System.Console.WriteLine(string.Join("-", messageParts));
            if (logMessage.Exception != null)
                System.Console.WriteLine(logMessage.Exception.ToString());

            return Task.CompletedTask;
        }
    }
}