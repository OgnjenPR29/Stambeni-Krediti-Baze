using StambeniKrediti.Connection;
using StambeniKrediti.Model;
using StambeniKrediti.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StambeniKrediti.DAO.Impl
{
    public class VrstaObjektaImpl : IVrstaObjektaDAO
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Delete(VrstaObjekta entity)
        {
            throw new NotImplementedException();
        }

        public int DeleteAll()
        {
            throw new NotImplementedException();
        }

        public int DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public bool ExistsById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<VrstaObjekta> FindAll()
        {

            string query = "select idvo, nazivvo from vrstaobjekta";
            List<VrstaObjekta> idVrsta = new List<VrstaObjekta>();


            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            VrstaObjekta vrstaObjekta = new VrstaObjekta(reader.GetInt32(0), reader.GetString(1));
                            idVrsta.Add(vrstaObjekta);
                        }
                    }
                }
            }

            return idVrsta;
        }

        public IEnumerable<VrstaObjekta> FindAllById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public VrstaObjekta FindById(int id)
        {
            throw new NotImplementedException();
        }

        public int Save(VrstaObjekta entity)
        {
            throw new NotImplementedException();
        }

        public int SaveAll(IEnumerable<VrstaObjekta> entities)
        {
            throw new NotImplementedException();
        }

        public string VrstaObjektaPoId(int ido)
        {
            string query = "select nazivvo from vrstaobjekta where idvo = :id_o ";
            string vrstaObjekta="";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "id_o", DbType.Int32);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "id_o", ido);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            vrstaObjekta = reader.GetString(0);
                        }
                    }
                }
            }
            return vrstaObjekta;
        }
    }
}
