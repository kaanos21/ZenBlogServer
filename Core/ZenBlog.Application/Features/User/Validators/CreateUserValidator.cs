using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Features.User.Commands;

namespace ZenBlog.Application.Features.User.Validators
{
    public class CreateUserValidator: AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidator() 
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Firstname is required");
            
        }
    }
}
