using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Base;
using ZenBlog.Application.Features.User.Commands;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.User.Handlers
{
    public class CreateUserCommandHandler(UserManager<AppUser> _userManager, IMapper _mapper) : IRequestHandler<CreateUserCommand, BaseResult<object>>
    {
        public async Task<BaseResult<object>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = new AppUser
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                UserName = request.UserName,
            };
            var result= await _userManager.CreateAsync(user,request.Password);

            if(!result.Succeeded)
            {
                return BaseResult<object>.Fail(result.Errors.Select(e=>e.Description));
            }

            return BaseResult<object>.Success("User is created succesfully");
        }
    }
}
