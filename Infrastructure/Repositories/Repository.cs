using BaseEntity.Domain.Entities;
using BaseEntity.Domain.Repositories;
using Messages.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public abstract class Repository<TEntity, TDbContext> : IRepository<TEntity>
        where TEntity : Entity
        where TDbContext : DbContext
    {
        protected TDbContext Context { get; }

        public DbSet<TEntity> DbSet => Context.Set<TEntity>();

        public Repository(TDbContext context) => Context = context;

        public virtual async Task<Maybe<TEntity>> FindAsync(Guid code) => await DbSet.SingleOrDefaultAsync(x => x.Code == code);

        public Task AddAsync(TEntity entity)
        {
            DbSet.Add(entity);
            return Task.CompletedTask;
        }

        public virtual Task RemoveAsync(TEntity entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
            return Task.CompletedTask;
        }

        public virtual Task UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual Task<List<TEntity>> GetAllAsync() => DbSet.ToListAsync();
    }
}
