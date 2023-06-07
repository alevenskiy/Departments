using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Departments
{ 
    internal class Client : IComparable<Client>, INotifyPropertyChanged
    {

        private static int staticId;

        static Client()
        {
            staticId = 0;
        }

        private static int NextId()
        {
            staticId++;
            return staticId;
        }

        private int id;
        private string department;
        private string surname;
        private string name;
        private string secondname;
        private string phone;
        private string passport;


        public int Id 
        { 
            get 
            { 
                return id; 
            } 
            set 
            { 
                id = value; 
            } 
        }
        public string Department 
        {
            get 
            { 
                return department; 
            }
            set 
            { 
                department = value;
                OnPropertyChanged("Department");
            }
        }
        public string Surname 
        {
            get
            {
                return surname;
            }
            set
            {
                surname = value;
                OnPropertyChanged("Surname");
            }
                
        }
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }

        }
        public string Secondname
        {
            get
            {
                return secondname;
            }
            set
            {
                secondname = value;
                OnPropertyChanged("Secondname");
            }

        }
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
                OnPropertyChanged("Phone");
            }

        }
        public string Passport
        {
            get
            {
                return passport;
            }
            set
            {
                passport = value;
                OnPropertyChanged("Passport");
            }

        }

        /// <summary>
        /// Создание клиента
        /// </summary>
        /// <param name="Department">ID департамента</param>
        /// <param name="Surname">Фамилия</param>
        /// <param name="Name">Имя</param>
        /// <param name="Secondname">Отчество</param>
        /// <param name="Phone">Телефон</param>
        /// <param name="Passport">Паспорт</param>
        public Client(string Department, string Surname, string Name, string Secondname, string Phone, string Passport)
        {
            this.Id = NextId();
            this.Department = Department;
            this.Surname = Surname;
            this.Name = Name;
            this.Secondname = Secondname;
            this.Phone = Phone;
            this.Passport = Passport;
        }

        public JObject Serialize()
        {
            JObject jObject = new JObject();
            jObject["Id"] = Id;
            jObject["Department"] = Department;
            jObject["Surname"] = Surname;
            jObject["Name"] = Name;
            jObject["Secondname"] = Secondname;
            jObject["Phone"] = Phone;
            jObject["Passport"] = Passport;

            return jObject;
        }

        public int CompareTo(Client other)
        {
            return String.Compare(this.Surname, other.Surname);
        }

        #region INotifyPropertyChanged inheritance 

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        #endregion
    }
}
