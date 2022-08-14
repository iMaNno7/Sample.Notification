using Domain.Entities;
using GenFu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Builder
{
    internal class ClientFactory
    {
        public List<Client> CreateListOfClients(int count)
        => A.ListOf<Client>(count);
    }
}
