using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Jammehcow.YosherBot.Common.Configurations;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.Command.Administration
{
    public class AdministrationCommandModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<AdministrationCommandModule> _logger;
        private readonly AdministrationOptions _administrationOptions;

        public AdministrationCommandModule(ILogger<AdministrationCommandModule> logger,
            AdministrationOptions administrationOptions)
        {
            _logger = logger;
            _administrationOptions = administrationOptions;
        }

        // TODO: move to own module, don't commit me with a snowflake literal
        [Command("disconnect")]
        [Summary("Disconnects and shuts down the bot")]
        public async Task Disconnect()
        {
            // TODO: replace with config reference
            if (Context.User.Id != 187814169813188608)
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
