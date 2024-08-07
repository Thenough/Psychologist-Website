﻿using Core.DTOs;
using Core.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public interface IProductServis:IService<Product>
    {
        Task<CustomResponseDTO<List<ProductWithCategoryDto>>> GetProductsWithCategory();
    }
}
