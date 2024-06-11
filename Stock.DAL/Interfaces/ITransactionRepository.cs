using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.Domain.Entities;

namespace Stock.DAL.Interfaces
{
    public interface ITransactionRepository : IBaseRepository<Transaction,int> 
    {
        // Product? GetFullById(int Id);

        // bool ExistByUnicityCriteria(Product product);
        // bool ExistById(int Id);
        // bool ExistByUnicityCriteriaAndNotSameId(int Id,Product product);
    }
}
