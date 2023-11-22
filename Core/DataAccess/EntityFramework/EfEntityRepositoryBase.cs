using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity,TContext>:IEntityRepository<TEntity>
        where TEntity : class,IEntity, new()
        where TContext : DbContext,new()
    {
        public void Add(TEntity entity)
        {
            //IDisposable pattern implementation of C#
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity); // Referansı yakala.    
                addedEntity.State = EntityState.Added; // Eklenecek nesne.
                context.SaveChanges(); // Yukardaki tüm işlemleri gerçekleştir(Addleme).
            }
        }

        public void Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity); // Referansı yakala.    
                deletedEntity.State = EntityState.Deleted; // Silinecek nesne.
                context.SaveChanges(); // Yukardaki tüm işlemleri gerçekleştir(Deleteleme).
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                // Veritabanındaki bütün tabloyu listeye çevirir ve onu geri verir.

                return filter == null // Filtre Null mı ?
                    ? context.Set<TEntity>().ToList() // Evetse bu çalışır.
                    : context.Set<TEntity>().Where(filter).ToList(); // Değilse filtreleyerek verir.
            }
        }

        public void Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity); // Referansı yakala.    
                updatedEntity.State = EntityState.Modified; // Güncellenecek nesne.
                context.SaveChanges(); // Yukardaki tüm işlemleri gerçekleştir(Updateleme).
            }
        }
    }
}
