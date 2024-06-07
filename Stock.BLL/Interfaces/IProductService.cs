using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;

namespace Stock.BLL.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAll();
        Product? GetById(int id);
        int GetCount();
        int Create(Product product);
        bool Update(int id, Product product);
        bool Delete(int id);
    }
}