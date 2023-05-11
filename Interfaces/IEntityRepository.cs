namespace Courses_HW_7_8.Interfaces
{
    public interface IEntityRepository<TEntity>
    {
        public Task<List<TEntity>> GetAllAsync();
        public Task AddAsync(TEntity entity);
        public Task DeleteAsync(TEntity entity);
        public Task UpdateAsync(TEntity entity);
        public Task SaveChangesAsync();
        public Task<TEntity> FindByIdAsync(int id);
        public Task<TEntity> FindByIdWithIncludesAsync(int id, params string[] includeNames);
        public Task<IEnumerable<TEntity>> Where(Func<TEntity, bool> predicate);
    }
}
