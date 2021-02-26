﻿using Autofac;
using Discord.Commands;

namespace Jammehcow.YosherBot.Console
{
    // ReSharper disable once UnusedType.Global
    public class ServiceRegistrationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            // TODO: move registrations to here

            builder.RegisterType<CommandService>()
                .SingleInstance();
        }
    }
}