using EmployeeData.Entities;
using EmployeeData.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRepository
{
    public interface IRepository<TEntity, TId>
             where TEntity : class , IEntity<TId>
    {
        IQueryable<TEntity> GetAll(bool overridQueryFilter = false);
        IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool overridQueryFilter = false);
        TEntity Get(TId id, bool overrideGlobalFilter = false);
        void Add(TEntity entity);
        void Update(TEntity entity);
     
     
        Task<int> SaveChanges();
        int SaveChangesSync();
    }
}
