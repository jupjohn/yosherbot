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
            logLevelLiteral = logLevelLiteral.ToUpperInvariant().PadRight(8);
            var messageParts = EnvironmentsHelper.IsDevelopment()
                ? new[] {$"[{logLevelLiteral}]", logMessage.Source, logMessage.Message}
                : new[] {$"[{logLevelLiteral}]", logMessage.Message};

            System.Console.WriteLine(string.Join(" - ", messageParts));
            if (logMessage.Exception != null)
                System.Console.WriteLine(logMessage.Exception.ToString());

            return Task.CompletedTask;
        }
    }
}