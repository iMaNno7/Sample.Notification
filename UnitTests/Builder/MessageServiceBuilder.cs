using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builder
{
    public class MessageServiceBuilder
    {
        private List<NotificationProvider> _notificationProviders;
        public MessageServiceBuilder()
        {
            _notificationProviders = new List<NotificationProvider>();
        }
        public MessageServiceBuilder WithNotificationProviders(NotificationProvider[] notificationProviders)
        {
            _notificationProviders.AddRange(notificationProviders);
            return this;
        }
        public MessageServiceBuilder WithNotificationProvider(NotificationProvider notificationProvider)
        {
            _notificationProviders.Add(notificationProvider);
            return this;
        }

        public MessageService Build()
            => new MessageService(_notificationProviders.ToArray());
    }
}
