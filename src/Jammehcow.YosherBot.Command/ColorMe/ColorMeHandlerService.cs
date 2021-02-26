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
            var generatedRoleName = RoleNameHelper.GetRoleNameFromUserId(Context.User.Id, ColorMeRolePrefix);

            if (!HexColourHelper.TryGetColourFromHexString(hexCode, out var colour))
            {
                await ReplyAsync($"Your hex code ``{hexCode}`` was invalid. Try something like #00FF00 or #abc");
                return;
            }

            // Attempt to resolve the role and create if it doesn't exist
            var resolvedRole = Context.Guild.Roles.SingleOrDefault(r => r.Name == generatedRoleName) ??
                               await CreateColorRoleAsync(Context.Guild, generatedRoleName);

            // Set the colour of the role to the one specified by the user
            var roleColour = new Optional<Color>(new Color(colour.R, colour.G, colour.B));
            var user = Context.Guild.GetUser(Context.User.Id);
            var userTopRolePosition = user.Roles.Max(r => r.Position);
            await resolvedRole.ModifyAsync(props =>
            {
                props.Color = roleColour;
                props.Hoist = false;
                props.Mentionable = false;
                props.Position = userTopRolePosition;
            });
            await user.AddRoleAsync(resolvedRole);
            await Context.Message.AddReactionAsync(new Emoji("\uD83D\uDC4D"));
        }

        [Command("uncolorme")]
        [Alias("uncolourme", "unsetcolor", "unsetcolour", "removecolor", "removecolour")]
        [Summary("Removes your colour")]
        // ReSharper disable once UnusedMember.Global
        public async Task HandleColourRemove()
        {
            var generatedRoleName = RoleNameHelper.GetRoleNameFromUserId(Context.User.Id, ColorMeRolePrefix);
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