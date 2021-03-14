using Microsoft.Extensions.Configuration;

namespace Jammehcow.YosherBot.Common.Configurations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class BotConfiguration : BaseConfiguration
    {
        private const string SectionName = "Bot";

        public BotConfiguration(IConfiguration configuration) : base(configuration, SectionName)
        {
        }

        public string Prefix { get; internal set; } = null!;
    }
}
