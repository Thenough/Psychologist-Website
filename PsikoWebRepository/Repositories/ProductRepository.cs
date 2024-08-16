using Core.Models.Concrete;
using Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Repository.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepositories
    {
        public ProductRepository(AppDbContext contex) : base(contex)
        {
        }

        public async Task<List<Product>> GetProductsWithCategory()
        {
            return await _context.Product.Include(x => x.Category).ToListAsync();
        }
    }
}
