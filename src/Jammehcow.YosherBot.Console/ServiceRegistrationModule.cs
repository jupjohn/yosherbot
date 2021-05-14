using Autofac;
using Jammehcow.YosherBot.Common.Configurations;
using Jammehcow.YosherBot.Common.Extensions;

namespace Jammehcow.YosherBot.Console
{
    // ReSharper disable once UnusedType.Global
    public class ServiceRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterOptionSection<GeneralOptions>();
            builder.RegisterOptionSection<ColorMeModuleOptions>();
        }
    }
}
