using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Features.Blogs.Commands;

namespace ZenBlog.Application.Features.Blogs.Validators
{
    public class UpdateBlogValidator:AbstractValidator<UpdateBlogCommand>
    {
        public UpdateBlogValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required");

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");

        }
    }
}
