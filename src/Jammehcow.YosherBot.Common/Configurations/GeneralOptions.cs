namespace Jammehcow.YosherBot.Common.Configurations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class GeneralOptions : IPrefixedOptions
    {
        public string ConfigSectionPrefix => "General";

        // ReSharper disable once AutoPropertyCanBeMadeGetOnly.Global
        public string Prefix { get; set; }
    }
}
