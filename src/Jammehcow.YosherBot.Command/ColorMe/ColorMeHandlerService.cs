using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
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
            // TODO: change to config subclass like "ColorMeModuleSettings"
            // This is nasty because now the whole class has access to every app setting including the
            //   bot token. Stop this
            _logger = logger;
            _colorMeConfig = colorMeConfig;
        }

        // TODO: move to own module, don't commit me with a snowflake literal
        [Command("quit")]
        public async Task Quit()
        {
            if (Context.User.Id != 187814169813188608)
                return;

            await Context.Message.AddReactionAsync(new Emoji("\uD83D\uDC4D"));
            await Context.Client.StopAsync();
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
                await RootHelpMessage();
                return;
            }

            _logger.LogInformation("Generated role name of {RoleName} for user {User}", generatedRoleName,
                Context.User.ToString());

            if (!HexColourHelper.TryGetColorFromHexString(hexCode, out var color))
            {
                _logger.LogWarning("User {User} entered invalid hex code {HexCode}", Context.User.ToString(), hexCode);
                await ReplyAsync($"Your hex code ``{hexCode}`` was invalid. Try something like #00FF00 or #abc");
                return;
            }

            _logger.LogInformation("Parsed hex code {HexCode} as RGB value ({R}, {G}, {B})", hexCode, color.R,
                color.G, color.B);

            // Set the colour of the role to the one specified by the user
            var roleColor = new Optional<Color>(new Color(color.R, color.G, color.B));

            // Attempt to resolve the role and create if it doesn't exist
            var possibleResolvedRole = Context.Guild.Roles.SingleOrDefault(r => r.Name == generatedRoleName);

            if (possibleResolvedRole?.Color == roleColor.Value)
            {
                _logger.LogInformation("User {GuildUserId} already has the requested color {ColorHex}", Context.User.Id,
                    hexCode);

                await Context.Message.ReplyAsync("You already have that color!");
                return;
            }

            _logger.LogInformation("Resolved role {RoleName} in guild {GuildId}", generatedRoleName, Context.Guild.Id);

            // Grab the user in the context of the guild
            var user = Context.Guild.GetUser(Context.User.Id);
            if (user == null)
            {
                _logger.LogError("Unable to find user with ID {UserId} in guild {GuildId}", Context.User.Id,
                    Context.Guild.Id);
                await ReplyAsync("An error occured trying to find you in the guild user list. Try again or wait " +
                                 "a minute for the list to complete");
                return;
            }

            // FIXME: this occasionally returns 0 when roles are present
            var userTopRolePosition = user.Roles.Max(r => r.Position);

            _logger.LogInformation("Top role for GuildUser {GuildUserId} is at position {RolePosition}", user.Id,
                userTopRolePosition);

            var resolvedRole = possibleResolvedRole ?? await CreateColorRoleAsync(Context.Guild, generatedRoleName);
            await resolvedRole.ModifyAsync(props =>
            {
                props.Color = roleColor;
                props.Hoist = false;
                props.Mentionable = false;
                props.Position = userTopRolePosition + 1;
            });

            _logger.LogInformation("Updated color and role position for role {RoleName}", generatedRoleName);

            if (user.Roles.All(r => r.Name != resolvedRole.Name))
            {
                _logger.LogInformation("Adding role {RoleName} to user {UserID}", generatedRoleName, user.Id);
                await user.AddRoleAsync(resolvedRole);
            }

            await Context.Message.AddReactionAsync(new Emoji("\uD83D\uDC4D"));
            _logger.LogInformation("Successfully handled colorme command for user {User}", user.ToString());
        }

        private static async Task<IRole> CreateColorRoleAsync(IGuild guild, string roleName)
        {
            // TODO: handle throws
            return await guild.CreateRoleAsync(roleName, isMentionable: false);
        }

        private async Task RootHelpMessage()
        {
            await ReplyAsync(
                "Help for `colorme`: \n" +
                "  - `$colorme` - shows this message\n" +
                "  - `$colorme help` - shows this message\n" +
                "  - `$colorme <hex code>` - colors your role with the hex code supplied (e.g. `#aabbcc`, `#abc`, `aabbcc` or `abc`)\n\n" +
                "You can remove your color using `$uncolorme`");
        }
    }
}
