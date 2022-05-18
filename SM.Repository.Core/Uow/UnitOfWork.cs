using Microsoft.EntityFrameworkCore;
using SM.Common;
using SM.Entity;
using SM.Models;
using SM.Repository.Core.Repositories.Base;
using SM.Repository.Core.Repositories.Interfaces;
using SM.Web.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SM.Repository.Core.Uow
{
    /// <summary>
    /// Unit of work repository for crud operations
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private SchoolManagementContext _schoolManagementContext;
        private Dictionary<Type, object> repos;

        public UnitOfWork(SchoolManagementContext schoolManagementContext)
        {
            _schoolManagementContext = schoolManagementContext;
        }
        public IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : class
        {
            if (repos == null)
            {
                repos = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!repos.ContainsKey(type))
            {
                repos[type] = new GenericRepository<TEntity>(_schoolManagementContext);
            }

            return (IGenericRepository<TEntity>)repos[type];
        }
        /// <returns>The number of objects in an Added, Modified, or Deleted state</returns>
        public int Commit()
        {
            return _schoolManagementContext.SaveChanges();
        }
        public User GetByEmail(User user)
        {
            try
            {
                User getUser = new User();
                getUser = _schoolManagementContext.Users.FirstOrDefault(x => x.EmailAddress == user.EmailAddress);
                return getUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public User GetUser(User user)
        {
            try
            {
                User getUser = new User();
                getUser = _schoolManagementContext.Users.FirstOrDefault(x => x.EmailAddress == user.EmailAddress && x.Password == user.Password);
                return getUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public IEnumerable<User> GetAll()
        {
            try
            {
                return _schoolManagementContext.Users.Where(x => x.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public User Register(User user)
        {
            try
            {
                User registerUser = new User();
                registerUser = _schoolManagementContext.Users.FirstOrDefault(x => x.EmailAddress == user.EmailAddress);
                if (registerUser == null)
                {
                    user.Password = string.Empty;
                    user.CreatedDate = DateTime.Now;
                    user.IsActive = false;
                    user.Role = "Admin";

                    _schoolManagementContext.Users.Add(user);
                    _schoolManagementContext.SaveChanges();
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public User GetUserPassword(User user)
        {
            try
            {
                User GetUserPassword = new User();
                GetUserPassword = _schoolManagementContext.Users.FirstOrDefault(x => x.UserId == user.UserId);
                return GetUserPassword;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public User SetUserPassword(User user)
        {
            try
            {
                User SetUserPassword = new User();
                SetUserPassword = _schoolManagementContext.Users.FirstOrDefault(x => x.UserId == user.UserId);

                SetUserPassword.Password = EncryptionDecryption.Encrypt(user.Password.ToString());
                SetUserPassword.IsActive = true;
                SetUserPassword.ModifiedDate = DateTime.Now;

                _schoolManagementContext.Entry(SetUserPassword).State = EntityState.Modified;
                _schoolManagementContext.SaveChanges();

                return SetUserPassword;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public User GetResetPassword(string id)
        {
            try
            {
                User GetResetPassword = new User();
                GetResetPassword = _schoolManagementContext.Users.FirstOrDefault(x => x.ResetPasswordCode == id);
                return GetResetPassword;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public User ResetPassword(ForgotPassword model)
        {
            try
            {
                User UpdateResetPassword = new User();
                UpdateResetPassword = _schoolManagementContext.Users.FirstOrDefault(x => x.ResetPasswordCode == model.ResetCode);

                UpdateResetPassword.Password = EncryptionDecryption.Encrypt(model.Password.ToString());

                _schoolManagementContext.Entry(UpdateResetPassword).State = EntityState.Modified;
                _schoolManagementContext.SaveChanges();

                return UpdateResetPassword;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public User ResetCode(string ResetCode)
        {
            try
            {
                User user = new User();
                user.ResetPasswordCode = ResetCode;
                _schoolManagementContext.SaveChanges();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
