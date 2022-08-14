using Domain;
using Domain.Entities;
using FluentAssertions;
using UnitTests.Builder;

namespace UnitTests
{
    public class MessageServiceTest
    {
        private ClientFactory _clientFactory;
        public MessageServiceTest()
        {
            _clientFactory = new ClientFactory();
        }

        [Theory]
        [InlineData(10,5)]
        public void endorse_clients_balanced_between_message_providers(int countClients,int balanceClients)
        {
            var providers = new NotificationProvider[] { new KavenegarProvider(), new FaraPayamakProvider() };
            var messageService = new MessageServiceBuilder()
                .WithNotificationProviders(providers)
                .Build();

            var clients = _clientFactory.CreateListOfClients(countClients);
            var res = messageService.SendMessage("test", clients);

            res.Keys.Should().BeEquivalentTo(providers);
            res.Values.Should().Contain(balanceClients);
        }
    }
}