using DankHappyBot.Service.Configuration;

namespace Jammehcow.YosherBot.Common.Configurations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ColorMeModuleConfiguration : IPrefixedOptions
    {
        public string GetPrefix() => "Module:ColourMe";

        public string RolePrefix { get; internal set; } = null!;
    }
}
