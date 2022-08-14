using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class MessageService
    {
        private List<Client> _clients=new();
        private Dictionary<string, string> _failedMessages=new();

        private NotificationProvider[] _notificationProvider;

        public MessageService(NotificationProvider[] activeNotificationProvider)
        {
            _notificationProvider = activeNotificationProvider;
        }

        public Dictionary<NotificationProvider, int> SendMessage(string message, List<Client> clientDtos)
        {            
            if (clientDtos.Any() is false)
                throw new ClientListNullException();
         
            var sentMessages = new Dictionary<NotificationProvider, int>();
            AddClients(clientDtos);
            var chunkClients = _clients.Chunk(GetBalanceProvider());

            int indexProvider = 0;
            foreach (var clients in chunkClients)
            {
                SendListMessage(message, clients, _notificationProvider[indexProvider]);
                sentMessages.Add(_notificationProvider[indexProvider], clients.Length);
                indexProvider++;
            }
            return sentMessages;
        }

        private void SendListMessage(string message, Client[] clients, NotificationProvider provider) {
            foreach (var client in clients)
            {
                var response = provider.Send(client.PhoneNumber, message);
                if (response is false)
                    _failedMessages.Add(message, client.PhoneNumber);
            }
        }

        private int GetBalanceProvider()
        =>
            (_clients.Count / _notificationProvider.Length);



        private void AddClients(List<Client> numbers)
        {
            _clients.AddRange(numbers);
        }

    }
}
