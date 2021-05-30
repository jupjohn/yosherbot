using Microsoft.Extensions.Logging;

namespace Jammehcow.YosherBot.EfCore.Repositories
{
    public class YosherBotRepository : Repository<YosherBotContext>
    {
        private readonly ILogger<YosherBotRepository> _logger;

        public YosherBotRepository(ILogger<YosherBotRepository> logger, YosherBotContext dbContext) : base(dbContext)
        {
            _logger = logger;
        }
    }
}
