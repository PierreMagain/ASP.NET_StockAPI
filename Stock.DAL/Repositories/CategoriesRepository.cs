using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using Stock.DAL.Interfaces;
using Stock.Domain.Entities;

namespace Stock.DAL.Repositories
{
    public class CategoriesRepository : BaseRepository<Categories, int>, ICategoriesRepository
    {
        public CategoriesRepository(SqlConnection conn) : base(conn,"Categories","Id")
        {
        }

        protected override Categories Convert(IDataRecord r)
        {
            Categories categories = new Categories()
            {
                Id = (int)r["Id"],
                Name = (string)r["Name"] 
            };
           
            return categories;
        }
        public override int Create(Categories c)
        {
            using SqlCommand cmd = _conn.CreateCommand();

             

            cmd.CommandText = @"INSERT INTO Categories (Name)
                                OUTPUT INSERTED.Id
                                VALUES (@name)";
            
            cmd.Parameters.AddWithValue("@name", c.Name);

            _conn.Open();

            int id = (int)cmd.ExecuteScalar(); 

            _conn.Close();

            return id;
        }

        public override bool Update(int Id, Categories c)
        {
            using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = @"UPDATE Product
                                SET Name = @name,
                                WHERE Id = @id";

            cmd.Parameters.AddWithValue("@id", c.Id);
            cmd.Parameters.AddWithValue("@name", c.Name);
            
            _conn.Open();

            int rowsAffected = cmd.ExecuteNonQuery();

            _conn.Close();
           
            return rowsAffected > 0;
        }

        public Categories? GetFullById(int Id)
        {
            using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = $@"SELECT *,
                                FROM 
                                    Categories c
                                WHERE 
                                    c.Id = @id;";
            
            cmd.Parameters.AddWithValue("@id",Id);

            _conn.Open();

            IDataReader r = cmd.ExecuteReader();

            Categories? c = null;

            if ( r.Read()){

                c = Convert(r);
            }

            _conn.Close();

            return c;
        }

        public bool ExistByName(string Name)
        {
          using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = $@"SELECT COUNT(*)
                                 FROM Categories
                                 WHERE Name = @name";
            
            cmd.Parameters.AddWithValue("@name",Name);

            _conn.Open();

            int count = (int)cmd.ExecuteScalar();

            _conn.Close();

            return count > 0;
        }

    }
}