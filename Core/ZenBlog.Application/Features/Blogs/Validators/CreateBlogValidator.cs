using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Features.Blogs.Commands;

namespace ZenBlog.Application.Features.Blogs.Validators
{
    public class CreateBlogValidator: AbstractValidator<CreateBlogCommand>
    {
        public CreateBlogValidator() 
        {
            RuleFor(b => b.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");
        }
    }
}
