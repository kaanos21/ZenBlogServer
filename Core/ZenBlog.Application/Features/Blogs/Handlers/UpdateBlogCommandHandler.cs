using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistance;
using ZenBlog.Application.Features.Blogs.Commands;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Blogs.Handlers
{
    public class UpdateBlogCommandHandler(IRepository<Blog> repository, IMapper _mapper, IUnitOfWork _unitOfWork) : IRequestHandler<UpdateBlogCommand, BaseResult<object>>
    {
        public async Task<BaseResult<object>> Handle(UpdateBlogCommand request, CancellationToken cancellationToken)
        {
            var blog=_mapper.Map<Blog>(request);
            repository.Update(blog);
            await _unitOfWork.SaveChangesAsync();
            
            return BaseResult<object>.Success("Succesfully updated");
        }
    }
}
