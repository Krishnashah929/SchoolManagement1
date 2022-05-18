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
    public interface IGenericRepository<T>
    {
        /// <returns>The Entity's state</returns>
        EntityState Add(T entity);
        IEnumerable<User> GetAll();
        User GetById(User user);
        User GetByEmail(User user);
        User Register(User user);
        User Update(User user);
        User Delete(User user);
        User GetUserPassword(User user);
        User SetUserPassword(User user);
        User GetResetPassword(string id);
        User ResetPassword(ForgotPassword model);
    }
}