using System;
using Jammehcow.YosherBot.Common.Helpers.Environment;
using Jammehcow.YosherBot.Console.Helpers;

namespace Jammehcow.YosherBot.UnitTest
{
    public static class TestHelper
    {
        public static void SetEnvironmentName(string value)
        {
            Environment.SetEnvironmentVariable(EnvironmentsHelper.Keys.Environment, value);
        }

        public static void SetBotToken(string value)
        {
            Environment.SetEnvironmentVariable(EnvironmentsHelper.Keys.DiscordToken, value);
        }
    }
}