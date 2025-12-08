using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Base;
using ZenBlog.Application.Contracts.Persistance;
using ZenBlog.Application.Features.Categories.Commands;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Categories.Handlers
{
    public class RemoveCategoryCommandHandler(IRepository<Category> repository, IUnitOfWork unitOfWork) : IRequestHandler<RemoveCategoryCommand, BaseResult<bool>>
    {
        public async Task<BaseResult<bool>> Handle(RemoveCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await repository.GetByIdAsync(request.id);

            if (category == null)
            {
                return BaseResult<bool>.NotFound("Category not found");
            }

            repository.Delete(category);
            var response = await unitOfWork.SaveChangesAsync();

            return response ? BaseResult<bool>.Success(true) : BaseResult<bool>.Fail("Failed to remove category");
        }
    }
}
