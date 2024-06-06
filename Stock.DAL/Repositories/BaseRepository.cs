using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.DAL.Interfaces;
using Stock.Domain.Entities;

namespace Stock.DAL.Repositories
{
    public abstract class BaseRepository<TEntity, TId> : IBaseRepository<TEntity, TId> where TEntity : class
    {
        private readonly string _tableName;
        private readonly string _columnIdName;
        protected readonly SqlConnection _conn;

        protected BaseRepository(SqlConnection conn, string tableName, string columnIdName)
        {
            _tableName = tableName;
            _columnIdName = columnIdName;
            _conn = conn;
        }

        protected abstract TEntity Convert(IDataRecord r);
        public IEnumerable<TEntity> GetAll()
        {
           using SqlCommand cmd = _conn.CreateCommand();
            cmd.CommandText = $@"SELECT * 
                                 FROM {_tableName}";

            _conn.Open();

            SqlDataReader r = cmd.ExecuteReader();

            while (r.Read())
            {
                yield return Convert(r);
            }

            _conn.Close();
        }

        public TEntity? GetById(TId id)
        {
            using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = $@"SELECT * 
                                 FROM {_tableName} 
                                 WHERE {_columnIdName} = @id";

            cmd.Parameters.AddWithValue("@id", id);

            _conn.Open();

            IDataReader r = cmd.ExecuteReader();

            TEntity? e = null;

            if (r.Read())
            {
                e = Convert(r);
            }

            _conn.Close();

            return e;
        }

        public int Count()
        {
            using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = $@"SELECT COUNT(*) 
                                 FROM {_tableName}";

            _conn.Open();

            int count = (int)cmd.ExecuteScalar();

            _conn.Close();

            return count;
        }

        public abstract TId Create(TEntity e);

        public abstract bool Update(TId id, TEntity e);

        public bool Delete(TId id)
        {
            using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = $@"DELETE {_tableName} 
                                 WHERE {_columnIdName} = @id";

            cmd.Parameters.AddWithValue("@id",id);

            _conn.Open();

            int nbRows = cmd.ExecuteNonQuery();

            _conn.Close();

            return nbRows == 1;
        }
    }
}