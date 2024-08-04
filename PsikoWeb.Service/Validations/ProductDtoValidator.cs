using Core.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Validations
{
    public class ProductDtoValidator:AbstractValidator<ProductDTO>
    {
        public ProductDtoValidator()
        {
            RuleFor(x=>x.Name).NotNull().WithMessage("{PropertyName} is not required").NotEmpty().WithMessage("{PropertyName} is not required");
            RuleFor(x => x.Price).InclusiveBetween(1,int.MaxValue).WithMessage("{PropertyName} must be greater");
            RuleFor(x => x.Stock).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater");
            RuleFor(x => x.CategoryId).InclusiveBetween(1, int.MaxValue).WithMessage("{PropertyName} must be greater");
        }
    }
}
