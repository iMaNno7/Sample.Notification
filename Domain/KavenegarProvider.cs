namespace Domain
{
    public class KavenegarProvider : NotificationProvider
    {
        public override bool Send(string phoneNumber, string message)
        {
            if (HealthCheckService())
            {
                Console.WriteLine($"send shod baw {nameof(KavenegarProvider)}");
                return true;
            }
            return false;
        }
        public override void StateChange(MessageService messageService)
        {
        }

    }
}