﻿using Courses_HW_7_8.DB.Enums;
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

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(TEntity entity)
        {
            _entity.Update(entity);
            return Task.CompletedTask;
        }
        public Task<TEntity> FindByIdWithIncludesAsync(int id, string[] includeNames)
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
        public Task FillCostCategoriesWithDefaultValues()
        {
            Array categories = Enum.GetValues(typeof(Categories));
            foreach (var category in categories)
            {
                _context.CostCategories.Add(new CostCategories() { Name = category.ToString() });
            }
             _context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
