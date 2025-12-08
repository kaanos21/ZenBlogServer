using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore; // DbSet, Include, ToListAsync, FindAsync vb. için gerekli
using ZenBlog.Application.Contracts.Persistance;
using ZenBlog.Domain.Entities.Common;
using ZenBlog.Persistence.Context;

namespace ZenBlog.Persistence.Concrete
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _appDbContext;
        private readonly DbSet<TEntity> _dbSet; 

        public GenericRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
            _dbSet = _appDbContext.Set<TEntity>();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.AsNoTracking().ToListAsync();
        }

        public IQueryable<TEntity> GetQuery()
        {
            return _dbSet;
        }

        public async Task<TEntity> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> GetSingleAsync(Expression<Func<TEntity, bool>> filter)
        {
            return await _dbSet.AsNoTracking().FirstOrDefaultAsync(filter);
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            var result=_dbSet.Find(entity.Id);
            _dbSet.Remove(entity);
        }
    }
}