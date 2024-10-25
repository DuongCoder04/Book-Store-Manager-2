using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using WindowsFormsApp.Models;

namespace WindowsFormsApp.DataService
{
    internal class Repository
    {
    }
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected DbSet<TEntity> Entities;
        private readonly DbContext _dbContext;

        /// <summary>  
        /// Initializes a new instance of the <see cref="BaseRepository{TEntity}"/> class.  
        /// Note that here I've stored Context.Set<TEntity>() in the constructor and store it in a private field like _entities.   
        /// This way, the implementation  of our methods would be cleaner:        ///   
        /// _entities.ToList();  
        /// _entities.Where();  
        /// _entities.SingleOrDefault();  
        /// </summary> 

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            Entities = _dbContext.Set<TEntity>();
        }
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = Entities;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }


        /// <summary>  
        /// Gets the specified identifier.  
        /// </summary>  
        /// <param name="id">The identifier.</param>  
        /// <returns></returns>  
        public virtual TEntity Get(int id)
        {
            // Here we are working with a DbContext, not specific DbContext.   
            // So we don't have DbSets we need to use the generic Set() method to access them.  
            return Entities.Find(id);
        }

        /// <summary>  
        /// Gets all.  
        /// </summary>  
        /// <returns></returns>  
        public IEnumerable<TEntity> GetAll()
        {
            return Entities.ToList();
        }

        /// <summary>  
        /// Finds the specified predicate.  
        /// </summary>  
        /// <param name="predicate">The predicate.</param>  
        /// <returns></returns>  
        public IEnumerable<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate);
        }

        /// <summary>  
        /// Singles the or default.  
        /// </summary>  
        /// <param name="predicate">The predicate.</param>  
        /// <returns></returns>  
        public TEntity SingleOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> predicate)
        {
            return Entities.Where(predicate).SingleOrDefault();
        }

        /// <summary>  
        /// First the or default.  
        /// </summary>  
        /// <returns></returns>  
        public TEntity FirstOrDefault()
        {
            return Entities.SingleOrDefault();
        }

        /// <summary>  
        /// Adds the specified entity.  
        /// </summary>  
        /// <param name="entity">The entity.</param>  
        public void Add(TEntity entity)
        {
            Entities.Add(entity);
            // Context.SaveChanges();
        }

        /// <summary>  
        /// Adds the range.  
        /// </summary>  
        /// <param name="entities">The entities.</param>  
        public void AddRange(IEnumerable<TEntity> entities)
        {
            Entities.AddRange(entities);
            //  Context.SaveChanges();
        }

        /// <summary>  
        /// Removes the specified entity.  
        /// </summary>  
        /// <param name="entity">The entity.</param>  
        public void Remove(TEntity entity)
        {
            Entities.Remove(entity);
            //  Context.SaveChanges();
        }

        /// <summary>  
        /// Removes the range.  
        /// </summary>  
        /// <param name="entities">The entities.</param>  
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Entities.RemoveRange(entities);
        }


        /// <summary>  
        /// Removes the Entity  
        /// </summary>  
        /// <param name="entityToDelete"></param>  
        public virtual void RemoveEntity(TEntity entityToDelete)
        {
            if (_dbContext.Entry(entityToDelete).State == EntityState.Detached)
            {
                Entities.Attach(entityToDelete);
            }
            Entities.Remove(entityToDelete);
            //  Context.SaveChanges();
        }

        /// <summary>  
        /// Update the Entity  
        /// </summary>  
        /// <param name="entityToUpdate"></param>  
        public virtual void UpdateEntity(TEntity entityToUpdate)
        {
            Entities.Attach(entityToUpdate);
            _dbContext.Entry(entityToUpdate).State = EntityState.Modified;
            //Context.SaveChanges();
        }
    }
    public class BillRepository : Repository<Bill>, IBillRepository
    {
        public BillRepository(DbContext context) : base(context) { }
    }
    public class BillDetailRepository : Repository<BillDetail>, IBillDetailRepository
    {
        public BillDetailRepository(DbContext context) : base(context) { }
    }
}