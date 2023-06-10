using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments
{
    internal class Manager : Consultant
    {
        public Manager() : base() { }

        public void DepartmentChange(Client client, string department)
        {
            client.Department = department;
        }

        public void SurnameChange(Client client, string surname)
        {
            client.Surname = surname;
        }

        public void NameChange(Client client, string name)
        {
            client.Name = name;
        }

        public void SecondnameChange(Client client, string secondname)
        {
            client.Secondname = secondname;
        }

        public void PassportChange(Client client, string passport)
        {
            client.Passport = passport;
        }

        public void AddClient(Client client)
        {
            clientlist.Add(client);
        }

        public void RemoveClient(Client client)
        {
            clientlist.Remove(client);
        }

        public void RemoveDepartment(string department)
        {
            ClientList clients = new ClientList();
            for(int i = 0; i < clientlist.Count; i++)
            {
                if (clientlist[i].Department != department)
                    clients.Add(clientlist[i]);
            }
            clientlist = clients;
        }

        public void RemoveAll()
        {
            clientlist.Clear();
        }
    }
}
