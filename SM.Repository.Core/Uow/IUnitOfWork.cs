using SM.Entity;
using SM.Models;
using SM.Repository.Core.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SM.Repository.Core.Uow
{
    /// <summary>
 /// Interface for unit of work repository for crud operations
 /// </summary>
    public interface IUnitOfWork
    {
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        int Commit();
        /// <returns>Repository</returns>
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class;
        IEnumerable<User> GetAll();
        User GetUser(User user);
        User GetByEmail(User user);
        User Register(User user);
        User GetUserPassword(User user);
        User SetUserPassword(User user);
        User GetResetPassword(string id);
        User ResetPassword(ForgotPassword model);
        User ResetCode(string ResetCode);
    }
}
