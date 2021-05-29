using System.Collections.Generic;

namespace Jammehcow.YosherBot.Common.Configurations
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class AdministrationOptions : IPrefixedOptions
    {
        public string ConfigSectionPrefix => "Administration";

        public List<ulong> PermittedUserIds { get; set; }
    }
}
