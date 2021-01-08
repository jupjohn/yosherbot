using System;
using MayBee;

namespace Jammehcow.YosherBot.Console.Helpers
{
    public static class EnvironmentsHelper
    {
        /// <summary>
        /// A static class for holding envvar key names
        /// </summary>
        internal static class Keys
        {
            public const string Environment = "DEPLOYMENT_ENVIRONMENT";
            public const string DiscordToken = "YOSHR_DISCORD_TOKEN";
        }

        public static bool IsDevelopment() => Environment.GetEnvironmentVariable(Keys.Environment) == null;
        public static bool IsProduction() => Environment.GetEnvironmentVariable(Keys.Environment)
            ?.Equals("production", StringComparison.InvariantCultureIgnoreCase) ?? false;

        /// <summary>
        /// Get the bot token to use when connecting to the Discord API
        /// </summary>
        /// <returns>A monad that could contain the token (if set)</returns>
        public static Maybe<string> GetDiscordBotToken() =>
            Maybe.FromNullable(Environment.GetEnvironmentVariable(Keys.DiscordToken));
    }
}