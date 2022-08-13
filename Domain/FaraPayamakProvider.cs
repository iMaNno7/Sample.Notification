namespace Domain
{
    public class FaraPayamakProvider : NotificationProvider
    {
        public override bool Send(string phoneNumber, string message)
        {
            if (HealthCheckService())
            {
                Console.WriteLine($"send shod baw {nameof(FaraPayamakProvider)}");
                return true;
            }
            return false;
        }

        public override void StateChange(MessageService messageService)
        {
            messageService.notificationProvider= new KavenegarProvider();
        }


    }
}