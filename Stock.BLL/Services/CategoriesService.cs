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
    public class CategoriesService : ICategoriesService
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesService(ICategoriesRepository categoriesRepository)
        {
            _categoriesRepository = categoriesRepository;
        }

        public IEnumerable<Categories> GetAll()
        {
            return _categoriesRepository.GetAll();
        }

        
    }
}