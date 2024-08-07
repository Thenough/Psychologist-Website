using AutoMapper;
using Core.DTOs;
using Core.Models.Concrete;
using Core.Repositories;
using Core.Services;
using Core.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class ProductServiceWithNoCaching : Service<Product>, IProductServis
    {
        private readonly IProductRepositories _productRepositories;
        private readonly IMapper _mapper;
        public ProductServiceWithNoCaching(IGenericRepository<Product> repository, IUnitOfWork unitOfWork, IMapper mapper, IProductRepositories productRepositories) : base(repository, unitOfWork)
        {
            _mapper = mapper;
            _productRepositories = productRepositories;
        }

       public async Task<CustomResponseDTO<List<ProductWithCategoryDto>>> GetProductsWithCategory()
        {
            var products = await _productRepositories.GetProductsWithCategory();
            var productsDto = _mapper.Map<List<ProductWithCategoryDto>>(products);
            return CustomResponseDTO<List<ProductWithCategoryDto>>.Success(200, productsDto);
        }
    }
}
