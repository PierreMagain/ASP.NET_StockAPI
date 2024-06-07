using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Stock.BLL.Interfaces;
using Stock.DAL.Interfaces;
using Stock.Domain.Entities;

namespace Stock.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }
        public Product? GetById(int id)
        {
            Product? product = _productRepository.GetFullById(id);
            if(product==null)
            {
                throw new KeyNotFoundException($"Product with {id} doesn't exist");
            }
            return product;
        }
        public int GetCount()
        {
            return _productRepository.Count();
        }

        public int Create(Product p)
        {
            if(_productRepository.ExistByUnicityCriteria(p))
            {
                throw new Exception("This product already exists");
            }
            return _productRepository.Create(p);
        }

        public bool Update(int id, Product p)
        {
            if (!_productRepository.ExistById(id))
            {
                throw new KeyNotFoundException($"Product with {id} doesn't exist");
            }
            if (_productRepository.ExistByUnicityCriteriaAndNotSameISBN(id,p))
            {
                throw new Exception("Error");
            }
            return _productRepository.Update(id,p);
        }
        public bool Delete(int id)
        {
            if (!_productRepository.ExistById(id))
            {
                throw new KeyNotFoundException($"Product with {id} doesn't exist");
            }
            return _productRepository.Delete(id);
        }

    }
}