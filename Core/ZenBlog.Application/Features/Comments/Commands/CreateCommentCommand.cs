using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Base;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Comments.Commands
{
    public record CreateCommentCommand: IRequest<BaseResult<object>>
    {
        public string UserId { get; init; }
        public string Body { get; init; }
        public Guid BlogId { get; init; }
        public DateTime CommentDate { get; init; }

    }
}
