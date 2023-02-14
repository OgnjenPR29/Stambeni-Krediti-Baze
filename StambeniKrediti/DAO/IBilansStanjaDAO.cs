using StambeniKrediti.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StambeniKrediti.DAO
{
    public interface IBilansStanjaDAO:ICRUDDao<BilansStanja, int>
    {
        Double UkupanDug(List<string> lica);
        BilansStanja FindByIdL(string idl);
    }
}
