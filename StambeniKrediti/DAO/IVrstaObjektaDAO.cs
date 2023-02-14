using StambeniKrediti.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StambeniKrediti.DAO
{
    public interface IVrstaObjektaDAO:ICRUDDao<VrstaObjekta, int>
    {
        string VrstaObjektaPoId(int ido);
    }
}
