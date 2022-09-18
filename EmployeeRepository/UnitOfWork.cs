using EmployeeData;
using EmployeeData.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace EmployeeRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        #region Members
        readonly EmployeeContext _context;
       
        #endregion

        #region Constructor
        public UnitOfWork(EmployeeContext context
           
            )
        {
            _context = context;
         
        }

        #endregion

        #region IUnitOfWork Members     

        public async Task<int> SaveChangesAsync()
        {
            SetIEntityFields();
            return await _context.SaveChangesAsync();
        }
        public int SaveChanges()
        {
            SetIEntityFields();
            return _context.SaveChanges();
        }

        public DbSet<TEntity> CreateSet<TEntity>()
           where TEntity : class
        {

            var set = _context.Set<TEntity>();
            return set;
        }

   
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            _context.Dispose();
        }

        #endregion

        #region Private Methods

        private void SetIEntityFields()
        {
            var now = DateTime.Now;
            
            _context.ChangeTracker.Entries<BaseEntity<int>>()
            .Where(e => e.State == EntityState.Added)
            .ToList()
            .ForEach(e =>
            {
                e.Entity.Created = now;
            });
     

            _context.ChangeTracker.Entries<BaseEntity<int>>()
            .Where(e => e.State == EntityState.Added)
            .ToList()
            .ForEach(e =>
            {
                e.Entity.IsDeleted = false;
            });

      

            _context.ChangeTracker.Entries<BaseEntity<int>>()
            .Where(e => e.State == EntityState.Modified)
            .ToList()
            .ForEach(e =>
            {
                e.Entity.Modified = now;
            });

        

         
     
        }

        #endregion

 


    }


}