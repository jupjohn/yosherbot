using DankHappyBot.Service.Configuration;

namespace Jammehcow.YosherBot.Common.Configurations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class ColorMeModuleOptions : IPrefixedOptions
    {
        public string GetPrefix() => "Module:ColourMe";

        public string RolePrefix { get; internal set; } = null!;
    }
}
