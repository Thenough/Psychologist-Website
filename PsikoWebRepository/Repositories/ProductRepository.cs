using Core.Models.Concrete;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepositories
    {
        public ProductRepository(AppDbContext contex) : base(contex)
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            return await _contex.Product.Include(x => x.Category).ToListAsync();
        }
    }
}
