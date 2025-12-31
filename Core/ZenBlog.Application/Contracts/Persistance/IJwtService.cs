using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Features.Comments.Result;
using ZenBlog.Application.Features.User.Results;

namespace ZenBlog.Application.Contracts.Persistance
{
    public interface IJwtService
    {
        Task<GetLoginQueryResult> GenerateTokenAsync(GetUserQueryResult result);
    }
}
