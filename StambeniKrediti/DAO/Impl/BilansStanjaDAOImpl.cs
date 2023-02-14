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
    public class BilansStanjaDAOImpl : IBilansStanjaDAO
    {
        public int Count()
        {
            throw new NotImplementedException();
        }

        public int Delete(BilansStanja entity)
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

        public IEnumerable<BilansStanja> FindAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BilansStanja> FindAllById(IEnumerable<int> ids)
        {
            throw new NotImplementedException();
        }

        public BilansStanja FindById(int id)
        {
            throw new NotImplementedException();
        }

        public int Save(BilansStanja entity)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return Save(entity, connection);
            }
        }

        private int Save(BilansStanja bilans, IDbConnection connection)
        {
            string updateSql = "update bilansstanja set idl=:idl_  , saldo= :saldo_ ," +
                " dug = :dug_ , kamata = :kamata_  where idbs = :idbs_";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = updateSql;
                ParameterUtil.AddParameter(command, "idl_", DbType.String, 13);
                ParameterUtil.AddParameter(command, "saldo_", DbType.Double);
                ParameterUtil.AddParameter(command, "dug_", DbType.Double);
                ParameterUtil.AddParameter(command, "kamata_", DbType.Double);
                ParameterUtil.AddParameter(command, "idbs_", DbType.Double);
                command.Prepare();

                ParameterUtil.SetParameterValue(command, "idbs_", bilans.IdBS);
                ParameterUtil.SetParameterValue(command, "idl_", bilans.IdL);
                ParameterUtil.SetParameterValue(command, "saldo_", bilans.Saldo);
                ParameterUtil.SetParameterValue(command, "dug_", bilans.Dug);
                ParameterUtil.SetParameterValue(command, "kamata_",bilans.Kamata);
                return command.ExecuteNonQuery();
            }
        }

        public int SaveAll(IEnumerable<BilansStanja> entities)
        {
            throw new NotImplementedException();
        }

        public double UkupanDug(List<string> lica)
        {
            Double ukupanDug = 0;

            StringBuilder sb = new StringBuilder();
            sb.Append("select sum(dug) from bilansstanja where idl in (");
            foreach (string idl in lica)
            {
                sb.Append(":idl_" + idl + ",");
            }
            sb.Remove(sb.Length - 1, 1); // delete last ','
            sb.Append(")");

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sb.ToString();
                    foreach (string idl in lica)
                    {
                        ParameterUtil.AddParameter(command, "idl_" + idl, DbType.String,10);
                    }
                    command.Prepare();

                    foreach (string idl in lica)
                    {
                        ParameterUtil.SetParameterValue(command, "idl_" + idl, idl) ;
                    }
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if(reader.Read())
                        {
                            ukupanDug += reader.GetDouble(0);
                        }
                    }
                }
            }
            return ukupanDug;
        }


        public BilansStanja FindByIdL(string idl)
        {
            string query = "select idbs, idl, saldo, dug, kamata from bilansstanja" +
                       " where idl = :id_l";
            BilansStanja bilans = null;

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "id_l", DbType.String, 10);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "id_l", idl);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            bilans = new BilansStanja(reader.GetInt32(0), reader.GetString(1),
                                reader.GetDouble(2), reader.GetDouble(3), reader.GetDouble(4));
                        }
                    }
                }
            }

            return bilans ;
        }

    }
}
