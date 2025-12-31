using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZenBlog.Application.Features.User.Results
{
    public class GetLoginQueryResult
    {
        public string Token { get; set; }
        public DateTime ExpirationTime { get; set; }
    }
}
