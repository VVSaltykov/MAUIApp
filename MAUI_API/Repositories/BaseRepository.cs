using Microsoft.EntityFrameworkCore;
using MAUI_API.Interfaces;

namespace MAUI_API.Repositories
{
    public class BaseRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly ApplicationContext applicationContext;
        private readonly DbSet<TEntity> entities;

        public BaseRepository(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
            entities = applicationContext.Set<TEntity>();
        }

        public virtual async Task AddToDatabase(TEntity entity)
        {
            entities.Add(entity);
            await applicationContext.SaveChangesAsync();
        }
    }
}
