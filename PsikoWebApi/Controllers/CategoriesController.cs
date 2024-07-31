using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PsikoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetSingleWithCategoryByIdProduct(int categoryId)
        {
            return Ok( await _categoryService.GetSingleCategoryByIdWithProductsAsync(categoryId));
        }
    }
}
