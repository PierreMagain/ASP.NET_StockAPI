using System.Data;
using System.Data.SqlClient;
using Stock.DAL.Interfaces;
using Stock.Domain.Entities;

namespace Stock.DAL.Repositories
{
    public class ProductRepository : BaseRepository<Product, int>, IProductRepository
    {
        public ProductRepository(SqlConnection conn) : base(conn,"Product","Id")
        {
        }

        protected override Product Convert(IDataRecord r)
        {
            return new Product()
            {
                Id = (int)r["Id"],
                Name = (string)r["Name"],
                Brand = r["Brand"] == DBNull.Value ? null : (string)r["Brand"],
                Description = r["Description"] == DBNull.Value ? null : (string)r["Description"],
                ExpiryDate = r["ExpiryDate"] == DBNull.Value ? null : (DateTime)r["ExpiryDate"],
                PriceExcludingTax = (decimal)r["PriceExcludingTax"],
                VAT = (decimal)r["VAT"],
                QuantityInStock = (int)r["QuantityInStock"],
                QuantityInShelf = (int)r["QuantityInShelf"]             
            };
        }
        public override int Create(Product p)
        {
            using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = $@"INSERT INTO Product
                                 OUTPUT INSERTED.Id
                                 VALUES(@id,@name,@brand,@description,@expiryDate,@priceExcludingTax,@vat,@quantityInStock,@quantityInShelf)";
            
            cmd.Parameters.AddWithValue("@id",p.Id);
            cmd.Parameters.AddWithValue("@name",p.Name);
            cmd.Parameters.AddWithValue("@brand",p.Brand == null ? DBNull.Value : p.Brand);
            cmd.Parameters.AddWithValue("@description",p.Description == null ? DBNull.Value : p.Description);
            cmd.Parameters.AddWithValue("@expiryDate",p.ExpiryDate == null ? DBNull.Value : p.ExpiryDate);
            cmd.Parameters.AddWithValue("@priceExcludingTax",p.PriceExcludingTax);
            cmd.Parameters.AddWithValue("@vat",p.VAT);
            cmd.Parameters.AddWithValue("@quantityInStock",p.QuantityInStock);
            cmd.Parameters.AddWithValue("@quantityInShelf",p.QuantityInShelf);

            _conn.Open();

            int id = (int)cmd.ExecuteScalar();

            _conn.Close();

            return id;
        }

        public override bool Update(int Id, Product p)
        {
            using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = $@"UPDATE Product
                                 SET Id = @newId,
                                     Name = @name,
                                     Brand = brand,
                                     Description = @description,
                                     ExpiryDate = @expiryDate,
                                     PriceExcludingTax = @priceExcludingTax,
                                     VAT = @vat,
                                     QuantityInStock = @quantityInStock,
                                     QuantityInShelf = @quantityInShelf
                                 WHERE Id like @id";
            
            cmd.Parameters.AddWithValue("@id",p.Id);
            cmd.Parameters.AddWithValue("@name",p.Name);
            cmd.Parameters.AddWithValue("@brand",p.Brand == null ? DBNull.Value : p.Brand);
            cmd.Parameters.AddWithValue("@description",p.Description == null ? DBNull.Value : p.Description);
            cmd.Parameters.AddWithValue("@expiryDate",p.ExpiryDate == null ? DBNull.Value : p.ExpiryDate);
            cmd.Parameters.AddWithValue("@priceExcludingTax",p.PriceExcludingTax);
            cmd.Parameters.AddWithValue("@vat",p.VAT);
            cmd.Parameters.AddWithValue("@quantityInStock",p.QuantityInStock);
            cmd.Parameters.AddWithValue("@quantityInShelf",p.QuantityInShelf);

            _conn.Open();

            int nbRows = cmd.ExecuteNonQuery();

            _conn.Close();

            return nbRows == 1;
        }

        public Product? GetFullById(int Id)
        {
            using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = $@"SELECT * 
                                 FROM Product P
                                 WHERE p.Id = @id";
            
            cmd.Parameters.AddWithValue("@id",Id);

            _conn.Open();

            IDataReader r = cmd.ExecuteReader();

            Product? p = null;

            if ( r.Read()){

                p = Convert(r);
            }

            _conn.Close();

            return p;
        }

        public bool ExistByUnicityCriteria(Product product)
        {
           using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = $@"SELECT COUNT(*)
                                 FROM Product
                                 WHERE Name = @name AND
                                       Brand = @brand AND
                                       ExpiryDate = @expiryDate";
            
            cmd.Parameters.AddWithValue("@name",product.Name);
            cmd.Parameters.AddWithValue("@brand",product.Brand);
            cmd.Parameters.AddWithValue("@expiryDate",product.ExpiryDate);

            _conn.Open();

            int count = (int)cmd.ExecuteScalar();

            _conn.Close();

            return count > 0;
        }

        public bool ExistById(int Id)
        {
          using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = $@"SELECT COUNT(*)
                                 FROM Product
                                 WHERE Id like @id";
            
            cmd.Parameters.AddWithValue("@id",Id);

            _conn.Open();

            int count = (int)cmd.ExecuteScalar();

            _conn.Close();

            return count > 0;
        }

        public bool ExistByUnicityCriteriaAndNotSameISBN(int Id, Product product)
        {
           using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = $@"SELECT COUNT(*)
                                 FROM Product
                                 WHERE Name = @name AND
                                       Brand = @brand AND
                                       ExpiryDate = @expiryDate AND
                                       Id not like @id";
            
            cmd.Parameters.AddWithValue("@name",product.Name);
            cmd.Parameters.AddWithValue("@brand",product.Brand);
            cmd.Parameters.AddWithValue("@expiryDate",product.ExpiryDate);
            cmd.Parameters.AddWithValue("@id",Id);

            _conn.Open();

            int count = (int)cmd.ExecuteScalar();

            _conn.Close();

            return count > 0;
        }
    }
}