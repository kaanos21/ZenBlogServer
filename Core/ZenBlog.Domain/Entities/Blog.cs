using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Domain.Entities.Common;

namespace ZenBlog.Domain.Entities
{
    public class Blog:BaseEntity
    {
        public string Title { get; set; }
        public string CoverImage { get; set; }
        public string BlogImage { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public string UserId { get; set; }
        public virtual IEnumerable<Comment> Comments { get; set; }

        public virtual Category Category { get; set; }
        public virtual AppUser User { get; set; }
    }
}
