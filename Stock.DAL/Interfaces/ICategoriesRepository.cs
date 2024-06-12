using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;

namespace Stock.DAL.Interfaces
{
    public interface ICategoriesRepository : IBaseRepository<Categories,int> 
    {
        Categories? GetFullById(int Id);
        bool ExistByName(string Name);
    }
}
