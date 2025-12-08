using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZenBlog.Application.Contracts.Persistance;
using ZenBlog.Persistence.Context;

namespace ZenBlog.Persistence.Concrete
{
    public class UnıtOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        public UnıtOfWork(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync()> 0;
        }
    }
}
