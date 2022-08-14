using Domain;
using Domain.Entities;

var phoneNumbers = new List<Client>{ new ("test","09111"),new ("test2", "09222"),new("test3","09333") , new ("test4", "09444") };
var message = "sample message";

MessageService messageService = new MessageService(new NotificationProvider[] {
new KavenegarProvider(),new FaraPayamakProvider()
});

messageService.SendMessage(message, phoneNumbers);
Console.ReadKey();


