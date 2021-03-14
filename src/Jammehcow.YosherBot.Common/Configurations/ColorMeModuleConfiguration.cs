using Microsoft.Extensions.Configuration;

namespace Jammehcow.YosherBot.Common.Configurations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ColorMeModuleConfiguration : BaseConfiguration
    {
        private const string SectionName = "Module:ColourMe";

        public ColorMeModuleConfiguration(IConfiguration configuration) : base(configuration, SectionName)
        {
        }

        public string RolePrefix { get; internal set; } = null!;
    }
}
