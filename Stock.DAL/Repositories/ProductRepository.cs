using System.Data;
using System.Data.SqlClient;
using Stock.DAL.Interfaces;
using Stock.Domain.Entities;

namespace Stock.DAL.Repositories
{
    public class ProductRepository : BaseRepository<Product, string>, IProductRepository
    {
        public ProductRepository(SqlConnection conn) : base(conn,"Product","Id")
        {
        }

        protected override Product Convert(IDataRecord r)
        {
            throw new NotImplementedException();
        }
        public override string Create(Product e)
        {
            throw new NotImplementedException();
        }

        public override bool Update(string id, Product e)
        {
            throw new NotImplementedException();
        }

        public Product? GetFullById(string Id)
        {
            throw new NotImplementedException();
        }

        public bool ExistByUnicityCriteria(Product product)
        {
            throw new NotImplementedException();
        }

        public bool ExistById(string Id)
        {
            throw new NotImplementedException();
        }
    }
}