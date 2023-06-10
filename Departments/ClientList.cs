using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Departments
{
    internal class ClientList : ObservableCollection<Client>
    {
        public ClientList GetDepartment(string department)
        {
            ClientList result = new ClientList();

            foreach(Client client in this)
            {
                if(client.Department == department)
                {
                    result.Add(client);
                }
            }

            return result;
        }

        public ClientList Deserialize(string path)
        {
            string str = "";

            try
            {
                using(Stream stream = File.Open(path, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    StreamReader streamReader = new StreamReader(stream);
                    str = streamReader.ReadToEnd();
                }
            }
            catch
            {
                MessageBox.Show("File does not open");
            }

            if (str != "")
                return JsonConvert.DeserializeObject<ClientList>(str);
            else
                return null;
        }

        public void Serialize(string path)
        {
            JArray array = new JArray();

            foreach (Client client in this)
                array.Add(client.Serialize());

            string str = array.ToString();

            try
            {
                File.WriteAllText(path, str);
            }
            catch
            {
                MessageBox.Show("File does not open");
            }
        }
    }
}
