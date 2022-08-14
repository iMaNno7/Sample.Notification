using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Client
    {
        public Client()
        {}
        public Client(string username, string phoneNumber)
        {
            Username = username;
            PhoneNumber = phoneNumber;
        }

        public string Username { get; set; }
        public string PhoneNumber { get; set; }
    }
}
