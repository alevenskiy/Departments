using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Departments
{
    internal class Consultant : IEmployee
    {
        protected ClientList clientlist;

        public Consultant() 
        { 
            clientlist = new ClientList(); 
        }

        public ClientList DownloadClients()
        {
            clientlist.Deserialize("clients.json");
            return clientlist;
        }

        public void SaveClients() 
        {
            clientlist.Serialize("clients.json");
        }

        public void PhoneChange(Client client, string phone)
        {
            client.Phone = phone;
        }
    }
}
