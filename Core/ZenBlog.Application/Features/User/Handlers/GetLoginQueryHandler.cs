using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistance;
using ZenBlog.Application.Features.User.Queries;
using ZenBlog.Application.Features.User.Results;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.User.Handlers
{
    public class GetLoginQueryHandler(UserManager<AppUser> _userManager,IJwtService _jwtService,IMapper _IMapper) : IRequestHandler<GetLoginQuery, BaseResult<GetLoginQueryResult>>
    {
        public async Task<BaseResult<GetLoginQueryResult>> Handle(GetLoginQuery request, CancellationToken cancellationToken)
        {
            var user=await _userManager.FindByEmailAsync(request.Email);
            if(user == null)
            {
                return BaseResult<GetLoginQueryResult>.Fail("User not found.");
            }

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if(!result)
            {
                return BaseResult<GetLoginQueryResult>.Fail("Invalid password.");
            }

            var userResult=_IMapper.Map<GetUserQueryResult>(user);

            var response = await _jwtService.GenerateTokenAsync(userResult);
            return BaseResult<GetLoginQueryResult>.Success(response);
        }
    }
}
