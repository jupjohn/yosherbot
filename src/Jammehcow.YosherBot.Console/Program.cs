namespace Jammehcow.YosherBot.Console
{
    public static class Program
    {
        public static void Main() => BotStartup.CreateDefaultBot().Run().GetAwaiter().GetResult();
    }
}