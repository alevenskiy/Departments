using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments
{ 
    internal class Client : IComparable<Client>
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

        public int Id { get; set; }
        public string Department { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Secondname { get; set; }
        public string Phone { get; set; }
        public string Passport { get; set; }

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
    }
}
