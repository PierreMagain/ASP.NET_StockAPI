using Stock.API.Models;
using Stock.Domain.Entities;

namespace Stock.API.Mappers
{
    public static class ProductMapper
    {
        public static Product ToProduct(this ProductFormDTO productDTO)
        {
            return new Product
            {
                Id = productDTO.Id,
                Name = productDTO.Name,
                Brand = productDTO.Brand,
                Description = productDTO.Description,
                ExpiryDate = productDTO.ExpiryDate,
                PriceExcludingTax = productDTO.PriceExcludingTax,
                VAT = productDTO.VAT,
                QuantityInStock = productDTO.QuantityInStock,
                QuantityInShelf = productDTO.QuantityInShelf,
            };
        }

        public static ProductDetailsDTO ToDetailsDTO(this Product product)
        {
            return new ProductDetailsDTO
            {
                Id = product.Id,
                Name = product.Name,
                Brand = product.Brand,
                Description = product.Description,
                ExpiryDate = product.ExpiryDate,
                PriceExcludingTax = product.PriceExcludingTax,
                VAT = product.VAT,
                QuantityInStock = product.QuantityInStock,
                QuantityInShelf = product.QuantityInShelf,
            };
        }

        public static ProductShortDTO ToShortDTO(this Product product)
        {
            return new ProductShortDTO
            {
                Id = product.Id,
                Name = product.Name,
                ExpiryDate = product.ExpiryDate,
            };
        }

        public static IEnumerable<ProductShortDTO> ToListDTO(this IEnumerable<Product> products)
        {
            List<ProductShortDTO> list = new List<ProductShortDTO>();

            foreach (Product item in products)
            {
                list.Add(item.ToShortDTO());
            }

            return list;
        }
    }
}
