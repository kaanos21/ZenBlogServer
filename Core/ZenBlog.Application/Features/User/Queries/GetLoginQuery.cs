using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Base;
using ZenBlog.Application.Features.User.Results;

namespace ZenBlog.Application.Features.User.Queries
{
    public class GetLoginQuery:IRequest<BaseResult<GetLoginQueryResult>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
