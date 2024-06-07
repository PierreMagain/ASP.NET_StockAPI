using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;

namespace Stock.BLL.Interfaces
{
    public interface IUserService
    {
        int Register(User u);
        User Login(string login, string password);
        string LoginToken(string login, string password);
    }
}
