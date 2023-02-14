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
    public class ObjekatService
    {
        private static readonly IObjekatDAO objekatDAO = new ObjekatDAOImpl();
        public int Count()
        {
            return objekatDAO.Count();
        }

        public int Delete(Objekat entity)
        {
            return DeleteById(entity.IdO);
        }

        public int DeleteAll()
        {
            return objekatDAO.DeleteAll();
        }

        public int DeleteById(int id)
        {
            return objekatDAO.DeleteById(id);
        }

        public bool ExistsById(int id)
        {
            return objekatDAO.ExistsById(id);
        }

        

        public List<Objekat> FindAll()
        {
            return objekatDAO.FindAll().ToList();
        }

        public List<Objekat> FindAllById(IEnumerable<int> ids)
        {
            return objekatDAO.FindAllById(ids).ToList(); 
        }

        public Objekat FindById(int id)
        {
            return objekatDAO.FindById(id);
        }



        public int Save(Objekat entity)
        {
            return objekatDAO.Save(entity);
        }

       


        public int SaveAll(IEnumerable<Objekat> entities)
        {
            return objekatDAO.SaveAll(entities);
        }

        public List<Objekat> objektiZadatogIDLa(string idl)
        {
            return objekatDAO.objektiZadatogIDLa(idl);
        }
    }
}
