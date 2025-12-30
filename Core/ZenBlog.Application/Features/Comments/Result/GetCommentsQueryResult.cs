using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Base;
using ZenBlog.Application.Features.Blogs.Results;
using ZenBlog.Application.Features.User.Results;
using ZenBlog.Domain.Entities;

namespace ZenBlog.Application.Features.Comments.Result
{
    public class GetCommentsQueryResult:BaseDto
    {
        public string UserId { get; set; }
        public string Body { get; set; }
        public GetUserQueryResult User { get; set; }
        public Guid BlogId { get; set; }
        public DateTime CommentDate { get; set; }

        //public virtual IEnumerable<SubComment> SubComments { get; set; }
        public  GetBlogsQueryResult Blog { get; set; }
    }
}
