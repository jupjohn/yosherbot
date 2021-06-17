using Autofac;
using Jammehcow.YosherBot.EfCore.Repositories;

namespace Jammehcow.YosherBot.EfCore
{
    public class ServiceRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<YosherBotRepository>();
        }
    }
}
