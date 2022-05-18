using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SM.Entity;
using SM.Models;

namespace SM.Repository.Core.Repositories.Interfaces
{
    /// <summary>
    /// Interface for generic repository for crud operations
    /// </summary>
    public interface IGenericRepository<T> where T : class
    {
        DbSet<T> DbSet { get; }
        void Add(T entity);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(int id);
    }
}