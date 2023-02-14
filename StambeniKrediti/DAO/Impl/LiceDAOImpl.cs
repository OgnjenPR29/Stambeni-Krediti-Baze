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
    public class LiceDAOImpl : ILiceDAO
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Delete(Lice entity)
        {
            throw new NotImplementedException();
        }

        public int DeleteAll()
        {
            throw new NotImplementedException();
        }

        public int DeleteById(string id)
        {
            throw new NotImplementedException();
        }

        public bool ExistsById(string idl)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return ExistsById(idl, connection);
            }
        }

        private bool ExistsById(string idl, IDbConnection connection)
        {
            string query = "select * from lice where idl=:id_l";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "id_l", DbType.String, 10);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "id_l", idl);
                return command.ExecuteScalar() != null;
            }
        }
        public IEnumerable<Lice> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Lice> FindAllById(IEnumerable<string> ids)
        {
            throw new NotImplementedException();
        }

        public Lice FindById(string id)
        {
            throw new NotImplementedException();
        }

        public Dictionary<string, List<Lice>> LicaPoVrsti()
        {
            Dictionary<string, List<Lice>> licaPoVrsti = new Dictionary<string, List<Lice>>();

            string query = "select idl, imel, przl, vrstal, mes_prihodil from lice where vrstal = 'FIZICKO' ";
            
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();
                    licaPoVrsti.Add("FIZICKO", new List<Lice>());
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Lice l = new Lice(reader.GetString(0), reader.GetString(1), reader.GetString(2),
                                  reader.GetString(3), reader.GetDouble(4)
                                );
                            licaPoVrsti["FIZICKO"].Add(l);
                        }
                    }
                }
            }
           query = "select idl, imel, przl, vrstal, mes_prihodil from lice where vrstal = 'PRAVNO' ";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();
                    licaPoVrsti.Add("PRAVNO", new List<Lice>());
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Lice l = new Lice(reader.GetString(0), reader.GetString(1), reader.GetString(2),
                                  reader.GetString(3), reader.GetDouble(4)
                                );
                            licaPoVrsti["PRAVNO"].Add(l);
                        }
                    }
                }
            }


            return licaPoVrsti;
        }

        public int Save(Lice entity)
        {
            throw new NotImplementedException();
        }

        public int SaveAll(IEnumerable<Lice> entities)
        {
            throw new NotImplementedException();
        }
    }
}
