using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistance;
using ZenBlog.Application.Features.Blogs.Queries;
using ZenBlog.Application.Features.Blogs.Results;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Blogs.Handlers
{
    public class GetBlogByIdQueryHandler(IRepository<Blog> repository, IMapper mapper) : IRequestHandler<GetBlogByIdQuery, BaseResult<GetBlogByIdQueryResult>>
    {
        public async Task<BaseResult<GetBlogByIdQueryResult>> Handle(GetBlogByIdQuery request, CancellationToken cancellationToken)
        {
            var value=await repository.GetByIdAsync(request.id);
            if(value == null)
            {
                return BaseResult<GetBlogByIdQueryResult>.NotFound("Blog not found");
            }
            return BaseResult<GetBlogByIdQueryResult>.Success(mapper.Map<GetBlogByIdQueryResult>(value));
        }
    }
}
