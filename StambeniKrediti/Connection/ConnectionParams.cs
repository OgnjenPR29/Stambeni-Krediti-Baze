using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StambeniKrediti.Connection
{
    public class ConnectionParams
    {

        //TODO: Ukoliko koristite SUBP u VM, a Visual Studio van VM promenite localhost sa IP adresom VM
        public static readonly string LOCAL_DATA_SOURCE = "//localhost:1521/xe";
      

        //TODO: promeniti username i password
        public static readonly string USER_ID = "student";
        public static readonly string PASSWORD = "ftn";
    }
}
