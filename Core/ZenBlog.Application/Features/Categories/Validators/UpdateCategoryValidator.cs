using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Features.Categories.Commands;

namespace ZenBlog.Application.Features.Categories.Validators
{
    public class UpdateCategoryValidator: AbstractValidator<UpdateCategoryCommand>
    {
        public UpdateCategoryValidator() 
        {
            RuleFor(x => x.CategoryName)
            .NotEmpty().WithMessage("Category name is required.")
            .MinimumLength(3).WithMessage("Category name must not exceed 3 characters.");

            RuleFor(x=>x.Id).NotEmpty().WithMessage("Category Id is required.");
        }
    }
}
