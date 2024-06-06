using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Domain.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; } = null!;
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string Source { get; set; } = null!;
        public string Destination { get; set; } = null!;
        public string Reason { get; set; } = null!;
    }
}