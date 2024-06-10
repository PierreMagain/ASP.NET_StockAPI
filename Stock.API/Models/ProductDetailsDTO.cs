using System.ComponentModel;

namespace Stock.API.Models
{
    public class ProductDetailsDTO
    {
        [DisplayName("Identifiant")]
        public int Id { get; set; }

        [DisplayName("Nom")]
        public string Name { get; set; } = null!;

        [DisplayName("Marque")]
        public string? Brand { get; set; }

        [DisplayName("Description")]
        public string? Description { get; set; }

        [DisplayName("Date d'expiration")]
        public DateTime? ExpiryDate { get; set; }

        [DisplayName("Prix HT")]
        public decimal PriceExcludingTax { get; set; }

        [DisplayName("TVA")]
        public decimal VAT { get; set; }

        [DisplayName("Quantité en stock")]
        public int QuantityInStock { get; set; }

        [DisplayName("Quantité en rayon")]
        public int QuantityInShelf { get; set; }
    }
}
