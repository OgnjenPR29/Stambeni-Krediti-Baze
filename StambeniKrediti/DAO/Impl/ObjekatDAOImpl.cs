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
    public class ObjekatDAOImpl : IObjekatDAO
    {
        public int Count()
        {
            string query = "select count(*) from objekat";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();

                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public int Delete(Objekat entity)
        {
            return DeleteById(entity.IdO);
        }

        public int DeleteAll()
        {
            string query = "delete from objekat";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.Prepare();
                    return command.ExecuteNonQuery();
                }
            }
        }

        public int DeleteById(int id)
        {
            string query = "delete from objekat where ido=:id_o";

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "id_o", DbType.Int32);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "id_o", id);
                    return command.ExecuteNonQuery();
                }
            }
        }

        public bool ExistsById(int id)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return ExistsById(id, connection);
            }
        }

        private bool ExistsById(int id, IDbConnection connection)
        {
            string query = "select * from objekat where ido=:id_o";

            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = query;
                ParameterUtil.AddParameter(command, "id_o", DbType.Int32);
                command.Prepare();
                ParameterUtil.SetParameterValue(command, "id_o", id);
                return command.ExecuteScalar() != null;
            }
        }

        public IEnumerable<Objekat> FindAll()
        {
            string query = "select ido, idl, idvo, povrsina, adresa, vrednost from objekat";
            List<Objekat> listaObjekata = new List<Objekat>();

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
                            Objekat objekat = new Objekat(reader.GetInt32(0), reader.GetString(1),
                                reader.GetInt32(2), reader.GetDouble(3), reader.GetString(4), reader.GetDouble(5));
                            listaObjekata.Add(objekat);
                        }
                    }
                }
            }

            return listaObjekata;
        }

        public IEnumerable<Objekat> FindAllById(IEnumerable<int> ids)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select ido, idl, idvo, povrsina, adresa, vrednost from objekat where ido in (");
            foreach (int id in ids)
            {
                sb.Append(":id" + id + ",");
            }
            sb.Remove(sb.Length - 1, 1); // delete last ','
            sb.Append(")");

            List<Objekat> listaObjekata = new List<Objekat>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = sb.ToString();
                    foreach (int id in ids)
                    {
                        ParameterUtil.AddParameter(command, "id" + id, DbType.Int32);
                    }
                    command.Prepare();

                    foreach (int id in ids)
                    {
                        ParameterUtil.SetParameterValue(command, "id" + id, id);
                    }
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Objekat objekat = new Objekat(reader.GetInt32(0), reader.GetString(1),
                                 reader.GetInt32(2), reader.GetDouble(3), reader.GetString(4), reader.GetDouble(5));
                            listaObjekata.Add(objekat);
                        }
                    }
                }
            }

            return listaObjekata;
        }

        public Objekat FindById(int id)
        {
            string query = "select ido, idl, idvo, povrsina, adresa, vrednost " +
                       "from objekat where ido = :id_o";
            Objekat objekat = null;

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "id_o", DbType.Int32);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "id_o", id);
                    using (IDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            objekat = new Objekat(reader.GetInt32(0), reader.GetString(1),
                                reader.GetInt32(2), reader.GetDouble(3), reader.GetString(4), reader.GetDouble(5));
                        }
                    }
                }
            }

            return objekat;
        }
        
        

        public int Save(Objekat entity)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                return Save(entity, connection);
            }
        }

        private int Save(Objekat objekat, IDbConnection connection)
        {
            string insertSql = "insert into objekat (idl, idvo, povrsina, adresa, vrednost, ido) " +
                "values (:idl_ , :idvo_ , :povrsina_ , :adresa_ , :vrednost_ , :ido_)";
            string updateSql = "update objekat set idl= :idl_ , idvo = :idvo_ , povrsina = :povrsina_ ," +
                " adresa = :adresa_ , vrednost = :vrednost_ where ido = :ido_";
             
            using (IDbCommand command = connection.CreateCommand())
            {
                command.CommandText = ExistsById(objekat.IdO, connection) ? updateSql : insertSql;
                ParameterUtil.AddParameter(command, "idl_", DbType.String, 50);
                ParameterUtil.AddParameter(command, "idvo_", DbType.Int32);
                ParameterUtil.AddParameter(command, "povrsina_", DbType.Double);
                ParameterUtil.AddParameter(command, "adresa_", DbType.String, 50);
                ParameterUtil.AddParameter(command, "vrednost_", DbType.Double);
                ParameterUtil.AddParameter(command, "ido_", DbType.Int32);
                command.Prepare();

                ParameterUtil.SetParameterValue(command, "ido_", objekat.IdO);
                ParameterUtil.SetParameterValue(command, "idl_", objekat.IdL);
                ParameterUtil.SetParameterValue(command, "idvo_", objekat.IdVO);
                ParameterUtil.SetParameterValue(command, "povrsina_", objekat.Povrsina);
                ParameterUtil.SetParameterValue(command, "adresa_", objekat.Adresa);
                ParameterUtil.SetParameterValue(command, "vrednost_", objekat.Vrednost);
                return command.ExecuteNonQuery();
            }
        }


        public int SaveAll(IEnumerable<Objekat> entities)
        {
            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                IDbTransaction transaction = connection.BeginTransaction(); 

                int numSaved = 0;

                foreach (Objekat entity in entities)
                {
                    numSaved += Save(entity, connection);
                }

                transaction.Commit();

                return numSaved;
            }
        }

        public List<Objekat> objektiZadatogIDLa(string idl)
        {
            string query = "select ido, idl, idvo, povrsina, adresa, vrednost " +
                       "from objekat where idl = :id_l";
            List<Objekat> objektiZadatogIDLA= new List<Objekat>();

            using (IDbConnection connection = ConnectionUtil_Pooling.GetConnection())
            {
                connection.Open();
                using (IDbCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    ParameterUtil.AddParameter(command, "id_l", DbType.String,50);
                    command.Prepare();
                    ParameterUtil.SetParameterValue(command, "id_l", idl);

                    using (IDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Objekat objekat = new Objekat(reader.GetInt32(0), reader.GetString(1),
                                reader.GetInt32(2), reader.GetDouble(3), reader.GetString(4), reader.GetDouble(5));
                            objektiZadatogIDLA.Add(objekat);
                        }
                    }
                }
            }

            return objektiZadatogIDLA;
        }
    }
}
