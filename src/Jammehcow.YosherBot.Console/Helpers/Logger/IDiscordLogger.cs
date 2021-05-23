using System.Threading.Tasks;
using Discord;

namespace Jammehcow.YosherBot.Console.Helpers.Logger
{
    public interface IDiscordLogger
    {
        Task HandleLogEventAsync(LogMessage logMessage);
    }
}