using Courses_HW_7_8.DB.Enums;
using Courses_HW_7_8.DB.Models;
using Courses_HW_7_8.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Courses_HW_7_8.DB
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity>  where TEntity : class, IEntity
    {
        private readonly DbSet<TEntity> _entity;

        private readonly AccountingDbContext _context;
        public AccountingDbContext Context { get => _context; }

        public EntityRepository(AccountingDbContext dbContext)
        {
            _context = dbContext;
            _entity = _context.Set<TEntity>();
        }
        public Task AddAsync(TEntity entity)
        {
            _context.Add(entity);
            return Task.CompletedTask;
        }

        public Task DeleteAsync(TEntity entity)
        {
            _context.Remove(entity);
            return Task.CompletedTask;
        }

        public Task<TEntity> FindByIdAsync(int id)
        {
            return _entity.FirstOrDefaultAsync(entity => entity.Id == id);
        }

        public Task<List<TEntity>> GetAllAsync()
        {
            return _entity.ToListAsync();
        }
        public Task<List<TEntity>> GetAllAsyncWithIncludes(params string[] includeNames)
        {
            IQueryable<TEntity> query = _entity;
            foreach (var includeName in includeNames)
            {
                query = query.Include(includeName);
            }
            return query.ToListAsync();

        }
        public Task<IEnumerable<TEntity>> Where(Func<TEntity, bool>  predicate)
        { 
            return Task.FromResult(_entity.Where(predicate));
        }
        public Task<bool> Any(Func<TEntity, bool> predicate)
        {
            return Task.FromResult(_entity.Any(predicate));
        }
        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(TEntity entity)
        {
            _entity.Update(entity);
            return Task.CompletedTask;
        }
        public Task<TEntity> FindByIdWithIncludesAsync(int id, params string[] includeNames)
        {
            if (includeNames == null)
            {
                throw new ArgumentNullException("Include names can't be null");
            }

            IQueryable<TEntity> query = _entity;
            foreach (var includeName in includeNames)
            {
                query = query.Include(includeName);
            }

            return query.FirstOrDefaultAsync(entity => entity.Id == id);
        }
    }
}
