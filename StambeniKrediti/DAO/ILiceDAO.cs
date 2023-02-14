using StambeniKrediti.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StambeniKrediti.DAO
{
    public interface ILiceDAO:ICRUDDao<Lice, string>
    {
        Dictionary<string, List<Lice>> LicaPoVrsti();
    }
}
