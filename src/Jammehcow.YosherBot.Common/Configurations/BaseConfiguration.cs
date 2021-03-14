using Microsoft.Extensions.Configuration;

namespace Jammehcow.YosherBot.Common.Configurations
{
    public class BaseConfiguration
    {
        protected BaseConfiguration(IConfiguration configuration, string section)
        {
            configuration.GetSection(section).Bind(this);
        }
    }
}
