using Core.DTOs;
using Core.Models.Concrete;

namespace Core.Services
{
    public interface ICategoryService : IService<Category>
    {
        public Task<CustomResponseDTO<CategoryWithProductsDto>> GetSingleCategoryByIdWithProductsAsync(int categoryId);
    }
}
