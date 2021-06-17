using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Jammehcow.YosherBot.Command.ColorMe.Helpers;
using Jammehcow.YosherBot.Common.Configurations;
using Jammehcow.YosherBot.EfCore.Repositories;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.Command.ColorMe
{
    // ReSharper disable once UnusedType.Global
    public class UnColorMeHandlerService : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<UnColorMeHandlerService> _logger;
        private readonly ColorMeModuleOptions _colorMeConfig;
        private readonly YosherBotRepository _repository;

        public UnColorMeHandlerService(ILogger<UnColorMeHandlerService> logger, ColorMeModuleOptions colorMeConfig,
            YosherBotRepository repository)
        {
            _logger = logger;
            _colorMeConfig = colorMeConfig;
            _repository = repository;
        }

        [Command("uncolorme")]
        [Alias("uncolourme", "unsetcolor", "unsetcolour", "removecolor", "removecolour")]
        [Summary("Removes your colour")]
        // ReSharper disable once UnusedMember.Global
        public async Task HandleColourRemove(string? subCommand = null)
        {
            _logger.LogInformation("Handling uncolorme command call for user {User}", Context.User.ToString());

            if (subCommand == "help")
            {
                _logger.LogInformation("Sending help message for {User}", Context.User.ToString());
                await ReplyWithHelpMessageAsync();
                return;
            }

            var generatedRoleName = RoleNameHelper.GetRoleNameFromUserId(Context.User.Id, _colorMeConfig.RolePrefix);

            _logger.LogInformation("Generated role name of {RoleName} for user {User}", generatedRoleName,
                Context.User.ToString());

            SocketRole? resolvedRole;
            try
            {
                resolvedRole = Context.Guild.Roles.SingleOrDefault(r => r.Name == generatedRoleName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to get roles in guild {GuildId} due to " +
                                     "an unexpected error: {ExceptionMessage}", Context.Guild.Id, ex.Message);
                await Context.Message.ReplyAsync("Something went wrong when trying to update your role. Try this command again.");
                return;
            }

            if (resolvedRole == null)
            {
                _logger.LogWarning("User {User} did not have the role {RoleName} assigned to them",
                    Context.User.ToString(), generatedRoleName);
                await Context.Message.ReplyAsync("You don't have a color; I can't remove something that doesn't exist!");
                return;
            }

            _logger.LogInformation("Deleting role {RoleName} for user {User}", generatedRoleName,
                Context.User.ToString());

            try {
                await resolvedRole.DeleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to delete a role with ID {RoleId} in guild {GuildId} due to " +
                                     "an unexpected error: {ExceptionMessage}",
                    resolvedRole.Id, Context.Guild.Id, ex.Message);
                await Context.Message.ReplyAsync("Something went wrong when trying to update your role. Try this command again.");
                return;
            }

            _logger.LogInformation("Role {RoleName} deleted!", generatedRoleName);

            try {
                await Context.Message.AddReactionAsync(new Emoji("\uD83D\uDC4D"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add reaction to message with with ID {MessageId}  in guild {GuildId} due to " +
                                     "an unexpected error: {ExceptionMessage}",
                    Context.Message.Id, Context.User.Id, ex.Message);
                await Context.Message.ReplyAsync("You're no longer colorful!");
            }
        }

        private async Task ReplyWithHelpMessageAsync()
        {
            await Context.Message.ReplyAsync("Help for `uncolorme`: \n" +
                                             "  - `$uncolorme` - removes the color from your name");
        }
    }
}
