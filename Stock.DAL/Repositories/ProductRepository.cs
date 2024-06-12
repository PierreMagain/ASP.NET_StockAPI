using System.ComponentModel;
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
            Product product= new Product()
            {
                Id = (int)r["Id"],
                Name = (string)r["Name"],
                Brand = r["Brand"] == DBNull.Value ? null : (string)r["Brand"],
                Description = r["Description"] == DBNull.Value ? null : (string)r["Description"],
                ExpiryDate = r["ExpiryDate"] == DBNull.Value ? null : (DateTime)r["ExpiryDate"],
                PriceExcludingTax = (decimal)r["PriceExcludingTax"],
                VAT = (decimal)r["VAT"],
                QuantityInStock = (int)r["QuantityInStock"],
                QuantityInShelf = (int)r["QuantityInShelf"],
                Categories = r["Categories"] == DBNull.Value ? null : ((string)r["Categories"]).Split(',').Select(c => new Categories { Name = c.Trim() }).ToList()        
            };
           
            return product;
        }
        public override int Create(Product p)
        {
            using SqlCommand cmd = _conn.CreateCommand();
            SqlTransaction transaction = null;

             _conn.Open();
            transaction = _conn.BeginTransaction();
            cmd.Transaction = transaction;

            cmd.CommandText = @"INSERT INTO Product (Name, Brand, Description, ExpiryDate, PriceExcludingTax, VAT, QuantityInStock, QuantityInShelf)
                                OUTPUT INSERTED.Id
                                VALUES (@name, @brand, @description, @expiryDate, @priceExcludingTax, @vat, @quantityInStock, @quantityInShelf)";
            
            cmd.Parameters.AddWithValue("@name", p.Name);
            cmd.Parameters.AddWithValue("@brand", p.Brand == null ? DBNull.Value : p.Brand);
            cmd.Parameters.AddWithValue("@description", p.Description == null ? DBNull.Value : p.Description);
            cmd.Parameters.AddWithValue("@expiryDate", p.ExpiryDate == null ? DBNull.Value : p.ExpiryDate);
            cmd.Parameters.AddWithValue("@priceExcludingTax", p.PriceExcludingTax);
            cmd.Parameters.AddWithValue("@vat", p.VAT);
            cmd.Parameters.AddWithValue("@quantityInStock", p.QuantityInStock);
            cmd.Parameters.AddWithValue("@quantityInShelf", p.QuantityInShelf);

            int productId = (int)cmd.ExecuteScalar();

            if (p.Categories != null && p.Categories.Count > 0)
            {
                foreach (var category in p.Categories)
                {
                    using SqlCommand catCmd = _conn.CreateCommand();
                    catCmd.Transaction = transaction;

                    catCmd.CommandText = @"INSERT INTO ProductCategories (ProductId, CategoryId)
                                        VALUES (@productId, (SELECT Id FROM Categories WHERE Name = @categoryName))";
                    catCmd.Parameters.AddWithValue("@productId", productId);
                    catCmd.Parameters.AddWithValue("@categoryName", category.Name);

                    catCmd.ExecuteNonQuery();
                }
            }

            transaction.Commit();

            return productId;
        }

        public override bool Update(int Id, Product p)
        {
            using SqlCommand cmd = _conn.CreateCommand();
            SqlTransaction transaction = null;
            _conn.Open();
            transaction = _conn.BeginTransaction();
            cmd.Transaction = transaction;

            cmd.CommandText = @"UPDATE Product
                                SET Name = @name,
                                    Brand = @brand,
                                    Description = @description,
                                    ExpiryDate = @expiryDate,
                                    PriceExcludingTax = @priceExcludingTax,
                                    VAT = @vat,
                                    QuantityInStock = @quantityInStock,
                                    QuantityInShelf = @quantityInShelf
                                WHERE Id = @id";

            cmd.Parameters.AddWithValue("@id", p.Id);
            cmd.Parameters.AddWithValue("@name", p.Name);
            cmd.Parameters.AddWithValue("@brand", p.Brand == null ? DBNull.Value : p.Brand);
            cmd.Parameters.AddWithValue("@description", p.Description == null ? DBNull.Value : p.Description);
            cmd.Parameters.AddWithValue("@expiryDate", p.ExpiryDate == null ? DBNull.Value : p.ExpiryDate);
            cmd.Parameters.AddWithValue("@priceExcludingTax", p.PriceExcludingTax);
            cmd.Parameters.AddWithValue("@vat", p.VAT);
            cmd.Parameters.AddWithValue("@quantityInStock", p.QuantityInStock);
            cmd.Parameters.AddWithValue("@quantityInShelf", p.QuantityInShelf);

            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected != 1)
            {
                throw new Exception("Update failed");
            }

            cmd.CommandText = @"DELETE FROM ProductCategories WHERE ProductId = @productId";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@productId", p.Id);
            cmd.ExecuteNonQuery();

            if (p.Categories != null && p.Categories.Count > 0)
            {
                foreach (var category in p.Categories)
                {
                    using SqlCommand catCmd = _conn.CreateCommand();
                    catCmd.Transaction = transaction;

                    catCmd.CommandText = @"INSERT INTO ProductCategories (ProductId, CategoryId)
                                        VALUES (@productId, (SELECT Id FROM Categories WHERE Name = @categoryName))";
                    catCmd.Parameters.AddWithValue("@productId", p.Id);
                    catCmd.Parameters.AddWithValue("@categoryName", category.Name);

                    catCmd.ExecuteNonQuery();
                }
            }

            transaction.Commit();
            return true;
        }

        public Product? GetFullById(int Id)
        {
            using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = $@"SELECT *,
                                (SELECT 
                                        STRING_AGG(c.Name, ', ') AS Categories
                                    FROM 
                                        Categories c
                                    JOIN 
                                        ProductCategories pc ON c.Id = pc.CategoryId
                                    WHERE 
                                        pc.ProductId = p.Id
                                ) AS Categories
                                FROM 
                                    Product p
                                WHERE 
                                    p.Id = @id;";
            
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
            cmd.Parameters.AddWithValue("@brand",product.Brand == null ? DBNull.Value : product.Brand);
            cmd.Parameters.AddWithValue("@expiryDate",product.ExpiryDate == null ? DBNull.Value : product.ExpiryDate);

            _conn.Open();

            int count = (int)cmd.ExecuteScalar();

            _conn.Close();

            return count > 0;
        }

        public bool ExistById(int id)
        {
          using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = $@"SELECT COUNT(*)
                                 FROM Product
                                 WHERE Id = @id";
            
            cmd.Parameters.AddWithValue("@id",id);

            _conn.Open();

            int count = (int)cmd.ExecuteScalar();

            _conn.Close();

            return count > 0;
        }

        public bool ExistByUnicityCriteriaAndNotSameId(int Id, Product product)
        {
           using SqlCommand cmd = _conn.CreateCommand();

            cmd.CommandText = $@"SELECT COUNT(*)
                                 FROM Product
                                 WHERE Name = @name AND
                                       Brand = @brand AND
                                       ExpiryDate = @expiryDate AND
                                       Id not like @id";
            
            cmd.Parameters.AddWithValue("@name",product.Name);
            cmd.Parameters.AddWithValue("@brand",product.Brand == null ? DBNull.Value : product.Brand);
            cmd.Parameters.AddWithValue("@expiryDate",product.ExpiryDate == null ? DBNull.Value : product.ExpiryDate);
            cmd.Parameters.AddWithValue("@id",Id);

            _conn.Open();

            int count = (int)cmd.ExecuteScalar();

            _conn.Close();

            return count > 0;
        }
    }
}