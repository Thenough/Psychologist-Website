using Core.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Repositories
{
    public interface IProductRepositories:IGenericRepository<Product>
    {
        Task<List<Product>> GetProductsWithCategory();
    }
}
