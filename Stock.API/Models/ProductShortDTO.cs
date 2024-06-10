using Stock.Domain.Entities;

namespace Stock.API.Models
{
    public class ProductShortDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public DateTime? ExpiryDate { get; set; }
    }
}
