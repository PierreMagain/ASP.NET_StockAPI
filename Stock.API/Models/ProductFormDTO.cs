using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Stock.API.Models
{
    public class ProductFormDTO
    {
        [DisplayName("Identifiant")]
        [Required]
        public int Id { get; set; }

        [DisplayName("Nom")]
        [Required(ErrorMessage = "Champ requis")]
        public string Name { get; set; } = null!;

        [DisplayName("Marque")]
        public string? Brand { get; set; }

        [DisplayName("Description")]
        public string? Description { get; set; }

        [DisplayName("Date d'expiration")]
        [DataType(DataType.DateTime)]
        public DateTime? ExpiryDate { get; set; }

        [DisplayName("Prix HT")]
        [Required(ErrorMessage = "Champ requis")]
        [Range(0, double.MaxValue, ErrorMessage = "Le prix doit être positif")]
        public decimal PriceExcludingTax { get; set; }

        [DisplayName("TVA")]
        [Required(ErrorMessage = "Champ requis")]
        [Range(0, double.MaxValue, ErrorMessage = "La TVA doit être positive")]
        public decimal VAT { get; set; }

        [DisplayName("Quantité en stock")]
        [Required(ErrorMessage = "Champ requis")]
        public int QuantityInStock { get; set; }

        [DisplayName("Quantité en rayon")]
        [Required(ErrorMessage = "Champ requis")]
        public int QuantityInShelf { get; set; }
    }
}
