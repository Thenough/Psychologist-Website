using AutoMapper;
using Core.DTOs;
using Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace PsikoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public CategoriesController(ICategoryService categoryService, IMapper mapper = null)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategory()
        {
            var categories = await _categoryService.GetAllAsync();
            var categoriesDto = _mapper.Map<List<CategoryDto>>(categories);
            return Ok(CustomResponseDTO<List<CategoryDto>>.Success(200,categoriesDto));
        }

        [HttpGet("[action]/{categoryId}")]
        public async Task<IActionResult> GetSingleWithCategoryByIdProduct(int categoryId)
        {
            return Ok(await _categoryService.GetSingleCategoryByIdWithProductsAsync(categoryId));
        }
    }
}
