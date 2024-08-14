using Core.DTOs;
using Core.Models.Concrete;

namespace Core.Services
{
    public interface IProductService : IService<Product>
    {
        Task<CustomResponseDTO<List<ProductWithCategoryDto>>> GetProductsWithCategory();
    }
}
