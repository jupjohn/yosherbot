using Discord.Commands;
using Discord.WebSocket;
using Moq;

namespace Jammehcow.YosherBot.UnitTest.Helpers
{
    public class TestableSocketContext
    {
        public readonly Mock<DiscordSocketClient> MockedClient = new();
        public readonly Mock<SocketUserMessage> MockedMessage = new();

        public SocketCommandContext Context => new(MockedClient.Object, MockedMessage.Object);
    }
}
