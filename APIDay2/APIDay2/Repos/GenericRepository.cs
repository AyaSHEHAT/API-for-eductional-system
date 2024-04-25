using APIDay2.Models;
using Microsoft.EntityFrameworkCore;
namespace APIDay2.Repos
{
        public class GenericRepository<TEntity> where TEntity : class
        {
            ITIContext db;

            public GenericRepository(ITIContext db)
            {
                this.db = db;
            }

            public List<TEntity> GetAll()
            {
                return db.Set<TEntity>().ToList();
            }

            public TEntity GetByID(int? id)
            {
                return db.Set<TEntity>().Find(id);
            }

            public void Add(TEntity entity)
            {
                db.Set<TEntity>().Add(entity);

            }

            public void Update(TEntity entity)
            {
                db.Entry(entity).State = EntityState.Modified;
            }

            public void Delete(int id)
            {
                TEntity obj = db.Set<TEntity>().Find(id);
                db.Set<TEntity>().Remove(obj);
            }

            public void Saving()
            {
                db.SaveChanges();
            }


        }
    }


