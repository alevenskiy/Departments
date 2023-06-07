using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Departments
{
    internal interface IEmployee
    {
        ClientList DownloadClients();

        void SaveClients();



    }
}
