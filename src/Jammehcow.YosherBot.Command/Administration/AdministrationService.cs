using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Jammehcow.YosherBot.Common.Configurations;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.Command.Administration
{
    public class AdministrationService : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<AdministrationService> _logger;
        private readonly AdministrationOptions _administrationOptions;

        public AdministrationService(ILogger<AdministrationService> logger,
            AdministrationOptions administrationOptions)
        {
            _logger = logger;
            _administrationOptions = administrationOptions;
        }

        [Command("disconnect")]
        [Alias("dc", "quit")]
        [Summary("Disconnects and shuts down the bot")]
        public async Task Disconnect()
        {
            if (!_administrationOptions.PermittedUserIds.Contains<ulong>(Context.User.Id))
            {
                _logger.LogInformation(
                    "User {UserId} tried to use command 'quit' but did not have administrative rights to do so",
                    Context.User.Id);
                return;
            }

            await Context.Message.AddReactionAsync(new Emoji("\uD83D\uDC4D"));
            await Context.Client.StopAsync();
        }
    }
}
