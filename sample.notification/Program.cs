var phoneNumbers = new string[] { "091111", "09222", "09333", "09444", "095555" };
var message = "sample message";
var activeProviders = ActiveProviders();

if (!activeProviders.Any())
{
    Console.WriteLine("hich provider faali mojod nist");
    return;
}
var provider = activeProviders.First();
MessageService messageService = new MessageService();
if (activeProviders.Count == 1)
{
    foreach (var phoneNumber in phoneNumbers)
    {
        messageService.Send(phoneNumber, message, provider);
    }
    return;
}

var ps = (phoneNumbers.Length * 50) / 100;
var lastPhoneNUmbers = new List<string>();
foreach (var phone in phoneNumbers.Take(ps))
{
    messageService.Send(phone, message, provider);
    lastPhoneNUmbers.Add(phone);
}
var nextProvider = GetProvider(provider.GetProviderName());
var lastActiveProvider = activeProviders.First(x => x.GetProviderName() == nextProvider);
foreach (var phone in phoneNumbers.Except(lastPhoneNUmbers))
{
    messageService.Send(phone, message, lastActiveProvider);
}
return;
Console.ReadKey();






Provider GetProvider(Provider provider)
{
    switch (provider)
    {
        case Provider.farapayamak:
            return Provider.rahyab;
        case Provider.rahyab:
            return Provider.farapayamak;
        default:
            return Provider.rahyab;
    }
}
// todo : break down
List<INotificationProvider> ActiveProviders()
{
    var notifires = AppDomain.CurrentDomain.GetAssemblies()
        .SelectMany(s => s.GetTypes())
        .Where(w => typeof(INotificationProvider).IsAssignableFrom(w) && w.IsClass && w.IsAbstract is false);

    List<INotificationProvider> providers = new();
    if (notifires is not null)
    {
        foreach (var notifire in notifires)
        {
            var notifireInstance = (INotificationProvider)Activator.CreateInstance(notifire);
            providers.Add(notifireInstance);
        }
        providers = providers.Where(w => w.IsActive == true).ToList();
        return providers;
    }
    return providers;
}









enum Provider
{
    farapayamak, rahyab
}


interface INotificationProvider
{
    bool IsActive { get; }
    void Send(string to, string message);
    Provider GetProviderName();
}

class FaraPayamakProvider : INotificationProvider
{
    public bool IsActive => true;

    public Provider GetProviderName()
    => Provider.farapayamak;

    public void Send(string to, string message)
    {
        Console.WriteLine($"{GetProviderName()} send {message} to {to}");
    }
}
class RahyabProvider : INotificationProvider
{
    public bool IsActive => true;

    public Provider GetProviderName()
    => Provider.rahyab;

    public void Send(string to, string message)
    {
        Console.WriteLine($"{GetProviderName()} send {message} to {to}");
    }
}

interface IMessageService
{
    void Send(string to, string message, INotificationProvider provider);
}

class MessageService : IMessageService
{
    public void Send(string to, string message, INotificationProvider provider)
    {
        provider.Send(to, message);
    }
}















