using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Departments
{
    internal class ClientList : IList<Client>
    {
        private Client[] clients;


        #region IList inheritance 


        /// <summary>
        /// Экземпляр Client, возвращает null если такого нет
        /// </summary>
        /// <param name="index">Позиция в листе</param>
        /// <returns>Client</returns>
        public Client this[int index]
        {
            get
            {
                return this.clients[index];
            }
            set
            {
                this.clients[index] = value;
            }
        }

        /// <summary>
        /// Число элементов в массиве
        /// </summary>
        public int Count => this.clients.Length;

        public bool IsReadOnly 
        {
            get
            {
                return false;
            }
        }

        public void Add(Client item)
        {
            if(clients == null)
            {
                clients = new Client[1];
                clients[0] = item;
            }
            else
            {
                Client[] newList = new Client[clients.Length + 1];
                for(int i = 0; i < clients.Length; i++)
                {
                    newList[i] = this.clients[i];
                }
                newList[newList.Length - 1] = item;
                clients = newList;
            }
        }

        public void Clear()
        {
            for(int i = 0; i < clients.Length; i++)
            {
                clients[i] = null;
            }
            clients = null;
        }

        public bool Contains(Client item)
        {
            for(int i = 0; i < clients.Length; i++)
            {
                if(clients[i].Equals(item))
                    return true;
            }
            return false;
        }

        public void CopyTo(Client[] array, int arrayIndex)
        {
            for(int i = 0; i < clients.Length; i++)
            {
                array[arrayIndex] = clients[i];
                arrayIndex++;
            }
        }

        public IEnumerator<Client> GetEnumerator()
        {
            return new ClientEnumerator(clients);
        }

        public int IndexOf(Client item)
        {
            for(int i = 0; i < clients.Length; i++)
            {
                if (clients[i].Equals(item))
                    return i;
            }
            return -1;
        }

        public void Insert(int index, Client item)
        {
            if(index >= 0 && index <= clients.Length)
            {
                Client[] newList = new Client[clients.Length + 1];
                for (int i = 0; i < clients.Length; i++)
                {
                    if(i < index)
                        newList[i] = this.clients[i];
                    else 
                        newList[i + 1] = this.clients[i];
                }
                newList[index] = item;
                clients = newList;
            }
        }

        public bool Remove(Client item)
        {
            int position = IndexOf(item);
            if (position != -1)
            {
                RemoveAt(position);
                return true;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if ((index >= 0) && (index < clients.Length))
            {
                Client[] newList = new Client[clients.Length - 1];

                for (int i = 0; i < clients.Length - 1; i++)
                {
                    if (i < index)
                        newList[i] = this.clients[i];
                    else
                        newList[i] = this.clients[i + 1];
                }
                clients = newList;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) GetEnumerator();
        }

        public class ClientEnumerator : IEnumerator<Client>
        {
            private Client[] data;
            private int pos = -1;

            public ClientEnumerator(Client[] data)
            {
                this.data = data;
            }

            public bool MoveNext()
            {
                pos++;
                return (pos < data.Length);
            }

            public void Reset()
            {
                pos = -1;
            }

            public void Dispose()
            {
                for(int i = 0; i < data.Length; i++)
                {
                    data[i] = null;
                }
                data = null;
            }

            public object Current
            {
                get
                {
                    if (pos == -1 || pos >= data.Length)
                    {
                        throw new InvalidOperationException();
                    }

                    return data[pos];
                }
            }

            Client IEnumerator<Client>.Current
            {
                get
                {
                    if (pos == -1 || pos >= data.Length)
                    {
                        throw new InvalidOperationException();
                    }

                    return data[pos];
                }
            }
        }

        #endregion

        public void Deserialize(string path)
        {
            string str = "";

            try
            {
                str = File.ReadAllText(path);
            }
            catch
            {
                MessageBox.Show("File does not open");
            }

            clients = JsonConvert.DeserializeObject<Client[]>(str);
        }

        public void Serialize(string path)
        {
            JArray array = new JArray();

            foreach (Client client in clients)
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
