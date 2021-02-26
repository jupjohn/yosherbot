using System.Threading.Tasks;
using Discord;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.Console.Helpers.Logger
{
    public class GenericDiscordLogger : IDiscordLogger
    {
        private readonly ILogger<GenericDiscordLogger> _logger;

        public GenericDiscordLogger(ILogger<GenericDiscordLogger> logger)
        {
            _logger = logger;
        }

        public Task HandleLogEventAsync(LogMessage logMessage)
        {
            var isDevelopment = EnvironmentsHelper.IsDevelopment();

            switch (logMessage.Severity)
            {
                case LogSeverity.Critical:
                    if (isDevelopment)
                        _logger.LogCritical("Discord.NET ({Source}) - {Message}", logMessage.Source, logMessage.Message);
                    else
                        _logger.LogCritical("Discord.NET - {Message}", logMessage.Message);
                    if (logMessage.Exception != null)
                        _logger.LogError("Exception: {Exception}", logMessage.Exception);
                    break;
                case LogSeverity.Error:
                    if (isDevelopment)
                        _logger.LogError("Discord.NET ({Source}) - {Message}", logMessage.Source, logMessage.Message);
                    else
                        _logger.LogError("Discord.NET - {Message}", logMessage.Message);
                    if (logMessage.Exception != null)
                        _logger.LogError("Exception: {Exception}", logMessage.Exception);
                    break;
                case LogSeverity.Warning:
                    if (isDevelopment)
                        _logger.LogWarning("Discord.NET ({Source}) - {Message}", logMessage.Source, logMessage.Message);
                    else
                        _logger.LogWarning("Discord.NET - {Message}", logMessage.Message);
                    if (logMessage.Exception != null)
                        _logger.LogError("Exception: {Exception}", logMessage.Exception);
                    break;
                case LogSeverity.Info:
                    if (isDevelopment)
                        _logger.LogInformation("Discord.NET ({Source}) - {Message}", logMessage.Source, logMessage.Message);
                    else
                        _logger.LogInformation("Discord.NET - {Message}", logMessage.Message);
                    break;
                case LogSeverity.Verbose:
                case LogSeverity.Debug:
                    if (isDevelopment)
                        _logger.LogDebug("Discord.NET ({Source}) - {Message}", logMessage.Source, logMessage.Message);
                    break;
                default:
                    _logger.LogInformation("Discord.NET ({Source}) - {Message}", logMessage.Source, logMessage.Message);
                    break;
            }

            return Task.CompletedTask;
        }
    }
}