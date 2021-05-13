using DankHappyBot.Service.Configuration;

namespace Jammehcow.YosherBot.Common.Configurations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GeneralConfiguration : IPrefixedOptions
    {
        public string GetPrefix() => "General";

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public string Prefix { get; set; }
    }
}
