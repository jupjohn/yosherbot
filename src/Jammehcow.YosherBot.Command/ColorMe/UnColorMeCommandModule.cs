using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Jammehcow.YosherBot.Command.ColorMe.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.Command.ColorMe
{
    public class UnColorMeCommandModule : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<UnColorMeCommandModule> _logger;
        private readonly IConfiguration _configuration;

        public UnColorMeCommandModule(ILogger<UnColorMeCommandModule> logger, IConfiguration configuration)
        {
            // TODO: change to config subclass like "ColorMeModuleSettings"
            // This is nasty because now the whole class has access to every app setting including the
            //   bot token. Stop this
            _logger = logger;
            _configuration = configuration;
        }

        [Command("uncolorme")]
        [Alias("uncolourme", "unsetcolor", "unsetcolour", "removecolor", "removecolour")]
        [Summary("Removes your colour")]
        // ReSharper disable once UnusedMember.Global
        public async Task HandleColourRemove()
        {
            _logger.LogInformation("Handling uncolorme command call for user {User}", Context.User.ToString());

            var generatedRoleName = RoleNameHelper.GetRoleNameFromUserId(Context.User.Id,
                _configuration["Module:ColorMe:RolePrefix"]);

            _logger.LogInformation("Generated role name of {RoleName} for user {User}", generatedRoleName,
                Context.User.ToString());

            var resolvedRole = Context.Guild.Roles.SingleOrDefault(r => r.Name == generatedRoleName);
            if (resolvedRole == null)
            {
                _logger.LogWarning("User {User} did not have the role {RoleName} assigned to them",
                    Context.User.ToString(), generatedRoleName);
                await ReplyAsync("You don't have a color; I can't remove something that doesn't exist!");
                return;
            }

            _logger.LogInformation("Deleting role {RoleName} for user {User}", generatedRoleName,
                Context.User.ToString());

            await resolvedRole.DeleteAsync();

            _logger.LogInformation("Role {RoleName} deleted!", generatedRoleName);

            await Context.Message.AddReactionAsync(new Emoji("\uD83D\uDC4D"));
        }
    }
}