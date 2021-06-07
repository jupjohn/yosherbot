using System.Threading.Tasks;
using Discord.Commands;

namespace Jammehcow.YosherBot.Command.ColorMe
{
    public partial class ColorRoleHandlerService
    {
        // TODO: allow people to get others' colors
        [Command("colorrole color")]
        [Alias("colourrole color")]
        public async Task GetColor()
        {
        }

        [Command("colorrole setcolor")]
        [Alias("colourrole setcolor")]
        public async Task SetColor(string? hexCode = null)
        {
        }
    }
}
