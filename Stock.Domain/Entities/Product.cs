using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Brand { get; set; }
        public string? Description { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public decimal PriceExcludingTax { get; set; }
        public decimal VAT { get; set; }
        public int QuantityInStock { get; set; }
        public int QuantityInShelf { get; set; }
        public List<Categories>? Categories { get; set; }
    }
}