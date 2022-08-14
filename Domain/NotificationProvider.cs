namespace Domain
{
    public abstract class NotificationProvider
    {
        public static bool IsActive { get; private set; }=true;
        public abstract bool Send(string phoneNumber, string message);
        public abstract void StateChange(MessageService messageService);
        protected bool HealthCheckService()
        {
            return IsActive;
        }

    }
}