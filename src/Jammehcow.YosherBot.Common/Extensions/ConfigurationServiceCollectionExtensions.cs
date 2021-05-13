using System;
using Autofac;
using Autofac.Builder;
using DankHappyBot.Service.Configuration;
using Microsoft.Extensions.Configuration;

namespace Jammehcow.YosherBot.Common.Extensions
{
    public static class ConfigurationServiceCollectionExtensions
    {
        /// <summary>
        /// Register a configuration section found in the registered IConfiguration instance
        /// </summary>
        /// <param name="containerBuilder">The AutoFac IOC container to resolve/register against</param>
        /// <typeparam name="TOptions">The type of options to register (containing a key)</typeparam>
        /// <returns>The registration data for that instance</returns>
        public static IRegistrationBuilder<IPrefixedOptions, SimpleActivatorData, SingleRegistrationStyle>
            RegisterConfiguration<TOptions>(this ContainerBuilder containerBuilder)
            where TOptions : class, IPrefixedOptions, new()
        {
            return containerBuilder.Register(ctx =>
            {
                var config = ctx.ResolveOptional<IConfiguration>();

                if (config == null)
                    throw new ArgumentException("No IConfiguration was registered in the ContainerBuilder");

                TOptions options = new();
                config.GetSection(options.GetPrefix()).Bind(options);

                return options;
            });
        }
    }
}
