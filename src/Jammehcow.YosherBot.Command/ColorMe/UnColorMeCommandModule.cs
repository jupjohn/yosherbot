using Discord.Commands;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.Command.ColorMe
{
    public class UnColorMeCommandModule : ModuleBase<SocketCommandContext>
    {
        private ILogger<UnColorMeCommandModule> _logger;
        private IConfiguration _configuration;

        public UnColorMeCommandModule(ILogger<UnColorMeCommandModule> logger, IConfiguration configuration)
        {
            // TODO: change to config subclass like "ColorMeModuleSettings"
            // This is nasty because now the whole class has access to every app setting including the
            //   bot token. Stop this
            _logger = logger;
            _configuration = configuration;
        }
    }
}