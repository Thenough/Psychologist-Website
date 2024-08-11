using Core.Models.Concrete;

namespace Core.Repositories
{
    public interface IProductRepositories : IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategory();
    }
}
