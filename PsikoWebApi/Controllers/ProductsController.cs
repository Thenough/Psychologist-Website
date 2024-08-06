using AutoMapper;
using Core.DTOs;
using Core.Models.Abstarct;
using Core.Models.Concrete;
using Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PsikoWebApi.Filter;

namespace PsikoWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IProductServis _service;
        public ProductsController(IMapper mapper, IService<Product> service, IProductServis productServis)
        {
            _mapper = mapper;
            _service = productServis;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetProductsWithCategory()
        {
            var productsWithCategory = await _service.GetProductsWithCategory();
            return Ok(productsWithCategory);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var product = await _service.GetAllAsync();
            var productsDtos = _mapper.Map<List<ProductDTO>>(product.ToList());
            return Ok(CustomResponseDTO<List<ProductDTO>>.Success(200,productsDtos));
        }

        [ServiceFilter(typeof(NotFoundFilter<>))]
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            var productsDto = _mapper.Map<ProductDTO>(product);
            return Ok(CustomResponseDTO<ProductDTO>.Success(200, productsDto));
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDTO productDTO)
        {
            var product = await _service.AddAsync(_mapper.Map<Product>(productDTO));
            var productDto = _mapper.Map<ProductDTO>(product);
            return StatusCode(201,productDto);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDTO productDto)
        {
            await _service.UpdateAsync(_mapper.Map<Product>(productDto));
            return Ok(CustomResponseDTO<ProductDTO>.Success(204));
        }

        [ServiceFilter(typeof(NotFoundFilter<>))]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Remove(int id)
        {
            var product = await _service.GetByIdAsync(id);
            await _service.RemoveAsync(product);
            return StatusCode(204);
        }
    }
}
