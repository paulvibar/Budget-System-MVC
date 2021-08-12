using BudgetSystem.Core.Contracts;
using BudgetSystem.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgetSystem.DataAccess.SQL
{
    public class SQLRepository<T> : IRepository<T> where T : BaseEntity
    {
        internal DataContext context;
        internal DbSet<T> dbSet;


        public SQLRepository(DataContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }
        IQueryable<T> IRepository<T>.Collection()
        {
            return dbSet;
        }

        void IRepository<T>.Commit()
        {
            context.SaveChanges();
        }

        void IRepository<T>.Delete(int Id)
        {
            var t = Find(Id);
            if(context.Entry(t).State == EntityState.Detached)
            {
                dbSet.Attach(t);
            }

            dbSet.Remove(t);
        }

        public T Find(int Id)
        {
            return dbSet.Find(Id);
        }

        void IRepository<T>.Insert(T t)
        {
            dbSet.Add(t);
        }

        void IRepository<T>.Update(T t)
        {
            dbSet.Attach(t);
            context.Entry(t).State = EntityState.Modified;
        }
    }
}
