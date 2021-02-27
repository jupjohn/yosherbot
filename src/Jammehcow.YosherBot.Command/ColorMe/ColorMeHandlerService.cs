using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Jammehcow.YosherBot.Command.ColorMe.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.Command.ColorMe
{
    public class ColorMeHandlerService : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<ColorMeHandlerService> _logger;
        private readonly IConfiguration _configuration;

        public ColorMeHandlerService(ILogger<ColorMeHandlerService> logger, IConfiguration configuration)
        {
            // TODO: change to config subclass like "ColorMeModuleSettings"
            // This is nasty because now the whole class has access to every app setting including the
            //   bot token. Stop this
            _logger = logger;
            _configuration = configuration;
        }

        [Command("colorme")]
        [Alias("colourme", "setcolor", "setcolour")]
        [Summary("Sets your colour using a hex code")]
        // ReSharper disable once UnusedMember.Global
        public async Task HandleColourSet([Summary("A full or shorthand hex code")] string hexCode)
        {
            // TODO: help commands, use subclasses?
            _logger.LogInformation("Handling colorme command call for user {User}", Context.User.ToString());

            var generatedRoleName = RoleNameHelper.GetRoleNameFromUserId(Context.User.Id,
                _configuration["Module:ColorMe:RolePrefix"]);

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


            // Attempt to resolve the role and create if it doesn't exist
            var resolvedRole = Context.Guild.Roles.SingleOrDefault(r => r.Name == generatedRoleName) ??
                               await CreateColorRoleAsync(Context.Guild, generatedRoleName);
            _logger.LogInformation("Resolved role {RoleName} in guild {GuildId}", generatedRoleName, Context.Guild.Id);

            // Set the colour of the role to the one specified by the user
            var roleColor = new Optional<Color>(new Color(color.R, color.G, color.B));

            var user = Context.Guild.GetUser(Context.User.Id);
            if (user == null)
            {
                _logger.LogError("Unable to find user with ID {UserId} in guild {GuildId}", Context.User.Id,
                    Context.Guild.Id);
                await ReplyAsync("An error occured trying to find you in the guild user list. Try again or wait " +
                                 "a minute for the list to complete");
                return;
            }

            var userTopRolePosition = user.Roles.Max(r => r.Position);

            _logger.LogInformation("Top role for GuildUser {GuildUserId} is at position {RolePosition}", user.Id,
                userTopRolePosition);

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

        [Command("uncolorme")]
        [Alias("uncolourme", "unsetcolor", "unsetcolour", "removecolor", "removecolour")]
        [Summary("Removes your colour")]
        // ReSharper disable once UnusedMember.Global
        public async Task HandleColourRemove()
        {
            var generatedRoleName = RoleNameHelper.GetRoleNameFromUserId(Context.User.Id,
                _configuration["Module:ColorMe:RolePrefix"]);
            var resolvedRole = Context.Guild.Roles.SingleOrDefault(r => r.Name == generatedRoleName);

            if (resolvedRole == null)
            {
                await ReplyAsync("You don't have a color; I can't remove something that doesn't exist!");
                return;
            }

            await resolvedRole.DeleteAsync();
            await Context.Message.AddReactionAsync(new Emoji("\uD83D\uDC4D"));
        }

        private static async Task<IRole> CreateColorRoleAsync(IGuild guild, string roleName)
        {
            // TODO: handle throws
            return await guild.CreateRoleAsync(roleName, isMentionable: false);
        }
    }
}