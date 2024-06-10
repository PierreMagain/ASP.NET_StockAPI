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
    public class UserRepository : BaseRepository<User, int>, IUserRepository
    {
        public UserRepository(SqlConnection conn) : base(conn, "User_", "Id")
        {
        }

        protected override User Convert(IDataRecord r)
        {
            return new User()
            {
                Id = (int)r["Id"],
                Username = (string)r["Username"],
                Email = (string)r["Email"],
                Password = (string)r["Password"],
            };
        }

        public override int Create(User u)
        {
            using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = @$"INSERT INTO User_ 
                                 OUTPUT INSERTED.Id 
                                 VALUES(@username,@email,@password)";

            cmd.Parameters.AddWithValue("@username", u.Username);
            cmd.Parameters.AddWithValue("@email", u.Email);
            cmd.Parameters.AddWithValue("@password", u.Password);

            _conn.Open();

            int newId = (int)cmd.ExecuteScalar();

            _conn.Close();

            return newId;
        }

        public override bool Update(int id, User e)
        {
            throw new NotImplementedException();
        }

        public bool ExistByUsername(string username)
        {
            using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = @$"SELECT COUNT(*) 
                                 FROM User_ 
                                 WHERE Username = @username";

            cmd.Parameters.AddWithValue("@username", username);

            _conn.Open();

            int count = (int)cmd.ExecuteScalar();

            _conn.Close();

            return count > 0;
        }

        public bool ExistByEmail(string email)
        {
            using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = @$"SELECT COUNT(*) 
                                 FROM User_ 
                                 WHERE Email = @email";

            cmd.Parameters.AddWithValue("@email", email);

            _conn.Open();

            int count = (int)cmd.ExecuteScalar();

            _conn.Close();

            return count > 0;
        }

        public User? GetUserByUsernameOrEmail(string login)
        {
            using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = @$"SELECT * 
                                 FROM User_ 
                                 WHERE Username LIKE @login OR Email LIKE @login";

            cmd.Parameters.AddWithValue("@login", login);

            _conn.Open();

            SqlDataReader r = cmd.ExecuteReader();
            

            User? user = null;

            if (r.Read())
            {
                user = Convert(r);
            }

            _conn.Close();

            return user;
        }
    }
}
