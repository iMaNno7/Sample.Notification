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
        private List<string> _clients;
        private Dictionary<string, string> _failedMessages;
        
        private NotificationProvider[] _activeNotificationProvider;
        public MessageService()
        {
            _activeNotificationProvider = new NotificationProvider[] { new KavenegarProvider() };
        }


        public void SendMessage(string message,List<string> clientPhoneNumbers)
        {
            if (_clients.Any() is false)
                throw new ClientListNullException();

            AddClients(clientPhoneNumbers);
            var chunkClients = _clients.Chunk(GetBalanceClients());

            int indexProvider = 0;
            foreach (var clients in chunkClients)
            {
                foreach (var client in clients)
                {
                    var response = _activeNotificationProvider[indexProvider].Send(client, message);
                    if (response is false)
                        _failedMessages.Add(message, client);
                }
                indexProvider++;
            }
        }

        private int GetBalanceClients()
        =>
            (_clients.Count / _activeNotificationProvider.Length);

        private void AddClients(List<string> numbers)
        {
            _clients.AddRange(numbers);
        }

    }
}
