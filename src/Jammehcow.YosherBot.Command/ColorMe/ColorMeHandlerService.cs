using System;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Jammehcow.YosherBot.Command.ColorMe.Helpers;
using Jammehcow.YosherBot.Common.Configurations;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.Command.ColorMe
{
    public class ColorMeHandlerService : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<ColorMeHandlerService> _logger;
        private readonly ColorMeModuleOptions _colorMeConfig;

        public ColorMeHandlerService(ILogger<ColorMeHandlerService> logger, ColorMeModuleOptions colorMeConfig)
        {
            _logger = logger;
            _colorMeConfig = colorMeConfig;
        }

        [Command("colorme")]
        [Alias("colourme", "setcolor", "setcolour")]
        [Summary("Sets your colour using a hex code")]
        // ReSharper disable once UnusedMember.Global
        public async Task HandleColourSet([Summary("A full or shorthand hex code")] string hexCode = "help")
        {
            // TODO: add permissions check, feature independent
            // TODO: help commands, use subclasses?
            _logger.LogInformation("Handling colorme command call for user {User}", Context.User.ToString());

            var generatedRoleName = RoleNameHelper.GetRoleNameFromUserId(Context.User.Id, _colorMeConfig.RolePrefix);

            if (hexCode == "help")
            {
                _logger.LogInformation("Sending help message for {User}", Context.User.ToString());
                await ReplyWithHelpMessageAsync();
                return;
            }

            _logger.LogInformation("Generated role name of {RoleName} for user {User}", generatedRoleName,
                Context.User.ToString());

            if (!HexColourHelper.TryGetColorFromHexString(hexCode, out var color))
            {
                _logger.LogWarning("User {User} entered invalid hex code {HexCode}", Context.User.ToString(), hexCode);
                await Context.Message.ReplyAsync($"Your hex code ``{hexCode}`` was invalid. Try something like #00FF00 or #abc");
                return;
            }

            _logger.LogInformation("Parsed hex code {HexCode} as RGB value ({R}, {G}, {B})", hexCode, color.R,
                color.G, color.B);

            // Set the colour of the role to the one specified by the user
            var roleColor = new Optional<Color>(new Color(color.R, color.G, color.B));

            // Attempt to resolve the role and create if it doesn't exist
            SocketRole? possibleResolvedRole;
            try
            {
                possibleResolvedRole = Context.Guild.Roles.SingleOrDefault(r => r.Name == generatedRoleName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to fetch roles in guild {GuildId} due to " +
                                     "an unexpected error: {ExceptionMessage}", Context.Guild.Id, ex.Message);
                await Context.Message.ReplyAsync("Something went wrong when trying to find a role. Try this command again.");
                return;
            }

            if (possibleResolvedRole?.Color == roleColor.Value)
            {
                _logger.LogInformation("User {GuildUserId} already has the requested color {ColorHex}", Context.User.Id,
                    hexCode);

                await Context.Message.ReplyAsync("You already have that color!");
                return;
            }

            _logger.LogInformation("Resolved role {RoleName} in guild {GuildId}", generatedRoleName, Context.Guild.Id);

            // Grab the user in the context of the guild
            // TODO: precache to prevent nulls
            var user = Context.Guild.GetUser(Context.User.Id);
            if (user == null)
            {
                _logger.LogError("Unable to find user with ID {UserId} in guild {GuildId}", Context.User.Id,
                    Context.Guild.Id);
                await Context.Message.ReplyAsync("An error occured trying to find you in the guild user list. Try again or wait " +
                                 "a minute for the list to complete");
                return;
            }

            // FIXME: this occasionally returns 0 when roles are present
            // Maybe lookup again after downloading guild stuff?
            var userTopRolePosition = user.Roles.Max(r => r.Position);

            _logger.LogInformation("Top role for GuildUser {GuildUserId} is at position {RolePosition}", user.Id,
                userTopRolePosition);

            IRole resolvedRole;
            try
            {
                resolvedRole = possibleResolvedRole ?? await CreateColorRoleAsync(Context.Guild, generatedRoleName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create a role in guild {GuildId} due to " +
                                     "an unexpected error: {ExceptionMessage}", Context.Guild.Id, ex.Message);
                await Context.Message.ReplyAsync("Something went wrong when trying to create your role. Try this command again.");
                return;
            }

            try {
                await resolvedRole.ModifyAsync(props =>
                {
                    props.Color = roleColor;
                    props.Hoist = false;
                    props.Mentionable = false;
                    props.Position = userTopRolePosition + 1;
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to modify a role with ID {RoleId} in guild {GuildId} due to " +
                                     "an unexpected error: {ExceptionMessage}", resolvedRole.Id, Context.Guild.Id,
                    ex.Message);
                await Context.Message.ReplyAsync("Something went wrong when trying to update your role. Try this command again.");
                return;
            }

            _logger.LogInformation("Updated color and role position for role {RoleName}", generatedRoleName);

            if (user.Roles.All(r => r.Name != resolvedRole.Name))
            {
                _logger.LogInformation("Adding role {RoleName} to user {UserID}", generatedRoleName, user.Id);

                try {
                    await user.AddRoleAsync(resolvedRole);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to add role with ID {RoleId} to user {UserId} in guild {GuildId} due to " +
                                         "an unexpected error: {ExceptionMessage}", resolvedRole.Id, Context.User.Id,
                        Context.Guild.Id, ex.Message);
                    await Context.Message.ReplyAsync("Something went wrong when trying to add your role. Try this command again.\n" +
                                     $"If this issue persists then ask a mod to add you to this role: `{generatedRoleName}`");
                    return;
                }
            }

            try {
                await Context.Message.AddReactionAsync(new Emoji("\uD83D\uDC4D"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to add reaction to message with with ID {MessageId}  in guild {GuildId} due to " +
                                     "an unexpected error: {ExceptionMessage}",
                    Context.Message.Id, Context.User.Id, ex.Message);
                await Context.Message.ReplyAsync("You're now colorful!");
                return;
            }

            _logger.LogInformation("Successfully handled colorme command for user {User}", user.ToString());
        }

        private static async Task<IRole> CreateColorRoleAsync(IGuild guild, string roleName)
        {
            return await guild.CreateRoleAsync(roleName, isMentionable: false);
        }

        private async Task ReplyWithHelpMessageAsync()
        {
            await Context.Message.ReplyAsync(
                "Help for `colorme`: \n" +
                "  - `$colorme` - shows this message\n" +
                "  - `$colorme help` - shows this message\n" +
                "  - `$colorme <hex code>` - colors your role with the hex code supplied (e.g. `#aabbcc`, `#abc`, `aabbcc` or `abc`)\n\n" +
                "You can remove your color using `$uncolorme`");
        }
    }
}
