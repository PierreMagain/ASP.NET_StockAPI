using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;

namespace Stock.DAL.Interfaces
{
    public interface IProductRepository : IBaseRepository<Product,string> 
    {
        Product? GetFullById(string Id);

        bool ExistByUnicityCriteria(Product product);
        bool ExistById(string Id);
        //bool ExistByUnicityCriteriaAndNotSameISBN(string isbn,Book book);
    }
}
