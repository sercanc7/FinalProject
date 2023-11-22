using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.EntityFramework
{
    //NuGet
    public class EfProductDal : IProductDal
    {
        public void Add(Product entity)
        {
            //IDisposable pattern implementation of C#
            using (NorthWindContext context = new NorthWindContext())
            {
                var addedEntity = context.Entry(entity); // Referansı yakala.    
                addedEntity.State = EntityState.Added; // Eklenecek nesne.
                context.SaveChanges(); // Yukardaki tüm işlemleri gerçekleştir(Addleme).
            }
        }

        public void Delete(Product entity)
        {
            using (NorthWindContext context = new NorthWindContext())
            {
                var deletedEntity = context.Entry(entity); // Referansı yakala.    
                deletedEntity.State = EntityState.Deleted; // Silinecek nesne.
                context.SaveChanges(); // Yukardaki tüm işlemleri gerçekleştir(Deleteleme).
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthWindContext context = new NorthWindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthWindContext context = new NorthWindContext())
            {
                // Veritabanındaki bütün tabloyu listeye çevirir ve onu geri verir.

                return filter == null // Filtre Null mı ?
                    ? context.Set<Product>().ToList() // Evetse bu çalışır.
                    : context.Set<Product>().Where(filter).ToList(); // Değilse filtreleyerek verir.
            }
        }

        public void Update(Product entity)
        {
            using (NorthWindContext context = new NorthWindContext())
            {
                var updatedEntity = context.Entry(entity); // Referansı yakala.    
                updatedEntity.State = EntityState.Modified; // Güncellenecek nesne.
                context.SaveChanges(); // Yukardaki tüm işlemleri gerçekleştir(Updateleme).
            }
        }
    }
}
