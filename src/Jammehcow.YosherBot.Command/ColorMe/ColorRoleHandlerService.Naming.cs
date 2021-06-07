using System.Threading.Tasks;
using Discord.Commands;

namespace Jammehcow.YosherBot.Command.ColorMe
{
    public partial class ColorRoleHandlerService
    {
        [Command("colorrole name")]
        [Alias("colourrole name")]
        public async Task GetName()
        {
        }

        [Command("colorrole setname")]
        [Alias("colourrole setname")]
        public async Task SetName(string newName)
        {
        }
    }
}
