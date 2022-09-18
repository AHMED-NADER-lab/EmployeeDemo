using EmployeeData.Entities;
using EmployeeData.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeRepository
{
    public class Repository<TEntity, TId> : IRepository<TEntity, TId>
           where TEntity : class, IEntity<TId>
    {
        private readonly IUnitOfWork _unitOfWork;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<TEntity> GetAll(bool overrideGlobaleFilters = false)
        {
            if (overrideGlobaleFilters)
            {
                return _unitOfWork.CreateSet<TEntity>().IgnoreQueryFilters();
            }
            return _unitOfWork.CreateSet<TEntity>();
        }

        public IQueryable<TEntity> GetAll_NotDelete(bool isDeleted = false)
        {
            return _unitOfWork.CreateSet<TEntity>().Where(x=>x.IsDeleted== isDeleted);
        }



        public IQueryable<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate, bool overrideGlobaleFilters = false)
        {
            if (overrideGlobaleFilters)
            {
                if (predicate != null)
                {
                    return GetAll().Where(predicate).IgnoreQueryFilters();
                }
                else
                {
                    throw new ArgumentNullException("The <predicate> paramter is required.");
                }
            }
            else
            {
                if (predicate != null)
                {
                    return GetAll().Where(predicate);
                }
                else
                {
                    throw new ArgumentNullException("The <predicate> paramter is required.");
                }
            }
        }
     
       
        public TEntity GetFirst(Expression<Func<TEntity, bool>> predicate, bool overrideGlobaleFilters = false)
        {
            if (predicate != null)
            {
                IQueryable<TEntity> query = GetAll(overrideGlobaleFilters).Where(predicate);
                return query.FirstOrDefault();
            }
            else
            {
                throw new ArgumentNullException("The <predicate> paramter is required.");
            }



        }

        public void Add(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }

            var dbSet = _unitOfWork.CreateSet<TEntity>();
            dbSet.Add(entity);

        }

        public void Update(TEntity entity)
        {

            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            var dbSet = _unitOfWork.CreateSet<TEntity>();
            dbSet.Update(entity);
        }

       
        public async Task<int> SaveChanges()
        {
            return await _unitOfWork.SaveChangesAsync();
        }

        public int SaveChangesSync()
        {
            return _unitOfWork.SaveChanges();
        }

        public TEntity Get(TId id, bool overrideGlobalFilter = false)
        {
            return GetAll(overrideGlobalFilter).FirstOrDefault(c => c.Id.Equals(id));
        }
    }
}