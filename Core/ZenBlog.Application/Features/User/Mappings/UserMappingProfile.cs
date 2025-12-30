using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Features.User.Commands;
using ZenBlog.Application.Features.User.Results;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.User.Mappings
{
    public class UserMappingProfile:Profile
    {
        public UserMappingProfile()
        {
            CreateMap<AppUser, CreateUserCommand>().ReverseMap();
            CreateMap<AppUser, GetUserQueryResult>().ReverseMap();
        }
    }
}
