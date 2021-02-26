using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Jammehcow.YosherBot.Command.ColorMe.Helpers;

namespace Jammehcow.YosherBot.Command.ColorMe
{
    public class ColorMeHandlerService : ModuleBase<SocketCommandContext>
    {
        // TODO: move to config
        private const string ColorMeRolePrefix = "colorme";

        [Command("colorme")]
        [Summary("Sets your colour using a hex code")]
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
        [Summary("Unsets your colour")]
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