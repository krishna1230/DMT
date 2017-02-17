using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using PLA.Communication.Database;
using System.Data;


namespace PLA.Communication
{
    public class DatabaseRepository<TEntity> where TEntity : class
    {
        //  private readonly DbSet<TEntity> 
        #region IFF Database
        db_PLAEntities context = new db_PLAEntities();

        DbSet<TEntity> dbSet;

        public DatabaseRepository()
        {
            this.dbSet = this.context.Set<TEntity>();
        }

        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);

        }
        public virtual void Update(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }
        public virtual void Delete(TEntity entity)
        {
            dbSet.Remove(entity);

        }
        public IQueryable<TEntity> Table
        {
            get { return dbSet.AsQueryable(); }
        }
        public List<TEntity> GetAll()
        {
            return dbSet.ToList();
        }
        public virtual void SaveChanges()
        {
            context.SaveChanges();
        }
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }
       
        #endregion




    }
}
