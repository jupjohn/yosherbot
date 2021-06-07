using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Jammehcow.YosherBot.Common.Configurations;
using Jammehcow.YosherBot.EfCore.Repositories;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.Command.ColorMe
{
    // ReSharper disable once UnusedType.Global
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public partial class ColorRoleHandlerService : ModuleBase<SocketCommandContext>
    {
        private readonly ILogger<ColorRoleHandlerService> _logger;
        private readonly YosherBotRepository _repository;
        private readonly GeneralOptions _generalOptions;

        public ColorRoleHandlerService(ILogger<ColorRoleHandlerService> logger, YosherBotRepository repository,
            GeneralOptions generalOptions)
        {
            _logger = logger;
            _repository = repository;
            _generalOptions = generalOptions;
        }

        [Command("colorrole")]
        [Alias("colourrole")]
        public Task DefaultCommand() => HelpCommand();

        [Command("colorrole help")]
        [Alias("colourrole help", "colorrole ?", "colourrole ?")]
        public async Task HelpCommand()
        {
            var prefix = _generalOptions.Prefix;
            // TODO: build these dynamically
            await Context.Message.ReplyAsync(
                "Help for `colorrole`: \n" +
                $"  - `{prefix}colorrole (help | ?)` - shows this message\n" +
                $"  - `{prefix}colorrole name` - changes or displays the name of your color role\n" +
                $"  - `{prefix}colorrole color` - sets or displays the color of your role (e.g. `#aabbcc`, `#abc`, `aabbcc` or `abc`)\n\n" +
                $"You can remove your role using `{prefix}uncolorme`");
        }
    }
}
