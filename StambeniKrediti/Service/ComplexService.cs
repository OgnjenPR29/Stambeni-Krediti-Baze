using StambeniKrediti.DAO;
using StambeniKrediti.DAO.Impl;
using StambeniKrediti.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StambeniKrediti.Service
{
    public class ComplexService
    {
        public static readonly IObjekatDAO objekatDAO = new ObjekatDAOImpl();
        public static readonly ILiceDAO liceDAO = new LiceDAOImpl();
        public static readonly IVrstaObjektaDAO vrstaObjektaDAO = new VrstaObjektaImpl();
        public static readonly IBilansStanjaDAO bilansStanjaDAO = new BilansStanjaDAOImpl();

        public List<Objekat> objektiZadatogIDLa(string idl)
        {
            return objekatDAO.objektiZadatogIDLa(idl);
        }
        public Dictionary<string, List<Lice>> LicaPoVrsti()
        {
            return liceDAO.LicaPoVrsti();
        }
        public string VrstaObjektaPoId(int ido)
        {
            return vrstaObjektaDAO.VrstaObjektaPoId(ido);
        }
        public double UkupanDug(List<string> lica)
        {
            return bilansStanjaDAO.UkupanDug(lica);
        }
        public bool ExistsById(string idl)
        {
            return liceDAO.ExistsById(idl);
        }
        public BilansStanja FindByIdL(string idl)
        {
            return bilansStanjaDAO.FindByIdL(idl);
        }

        public int SaveBilans(BilansStanja bilans)
        {
            return bilansStanjaDAO.Save(bilans);
        }
        public List<VrstaObjekta> GetVrstaObjekta()
        {
            return vrstaObjektaDAO.FindAll().ToList();
        }
    }
}
