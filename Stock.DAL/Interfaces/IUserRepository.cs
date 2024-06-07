using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;

namespace Stock.DAL.Interfaces
{
    public interface IUserRepository : IBaseRepository<User,int>
    {
        bool ExistByUsername(string username);
        bool ExistByEmail(string email);
        User? GetUserByUsernameOrEmail(string login);
    }
}
