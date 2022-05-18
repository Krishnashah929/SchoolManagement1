using Microsoft.EntityFrameworkCore;
using SM.Common;
using SM.Entity;
using SM.Models;
using SM.Repository.Core.Repositories.Interfaces;
using SM.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Repository.Core.Repositories.Base
{
    /// <summary>
    ///generic repository for crud operations
    /// </summary>
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly SchoolManagementContext schoolManagementContext;
        private readonly DbSet<T> databaseSet;

        public GenericRepository(SchoolManagementContext schoolManagementContext)
        {
            this.schoolManagementContext = schoolManagementContext;
            databaseSet = schoolManagementContext.Set<T>();
        }
        public DbSet<T> DbSet
        {
            get
            {
                return schoolManagementContext.Set<T>();
            }
        }
        public void Add(T entity)
        {
            try
            {
                schoolManagementContext.Add(entity);
                schoolManagementContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task AddAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            try
            {
                schoolManagementContext.Remove(id);
                schoolManagementContext.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
} 

