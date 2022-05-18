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
        public virtual EntityState Add(T entity)
        {
            return databaseSet.Add(entity).State;
        }
        public IEnumerable<User> GetAll()
        {
            try
            {
                return schoolManagementContext.Users.Where(x => x.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public User GetByEmail(User user)
        {
            try
            {
                User getUser = new User();
                getUser = schoolManagementContext.Users.FirstOrDefault(x => x.EmailAddress == user.EmailAddress);
                return getUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public User GetById(User user)
        {
            try
            {
                User getUser = new User();
                getUser = schoolManagementContext.Users.FirstOrDefault(x => x.UserId == user.UserId);
                return getUser;
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
                registerUser = schoolManagementContext.Users.FirstOrDefault(x => x.EmailAddress == user.EmailAddress);
                if (registerUser == null)
                {
                    user.Password = string.Empty;
                    user.CreatedDate = DateTime.Now;
                    user.IsActive = false;
                    user.Role = "Admin";

                    schoolManagementContext.Users.Add(user);
                    schoolManagementContext.SaveChanges();
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public User Delete(User user)
        {
            try
            {
                User deleteUser = new User();
                deleteUser = schoolManagementContext.Users.FirstOrDefault(x => x.UserId == user.UserId);

                if (deleteUser != null)
                {
                    if (deleteUser.IsDelete == false)
                    {
                        deleteUser.IsDelete = true;
                        deleteUser.IsActive = false;
                    }

                    schoolManagementContext.SaveChanges();
                }
                return deleteUser;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public User Update(User user)
        {
            try
            {
                User updateUser = new User();

                updateUser = schoolManagementContext.Users.FirstOrDefault(x => x.UserId == user.UserId);

                if (updateUser != null)
                {
                    if (updateUser.FirstName != user.FirstName)
                    {
                        updateUser.FirstName = user.FirstName;
                    }

                    if (updateUser.Lastname != user.Lastname)
                    {
                        updateUser.Lastname = user.Lastname;
                    }

                    if (updateUser.EmailAddress != user.EmailAddress)
                    {
                        updateUser.EmailAddress = user.EmailAddress;
                    }

                    schoolManagementContext.Entry(updateUser).State = EntityState.Modified;

                    schoolManagementContext.SaveChanges();
                }
                return updateUser;
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
                GetUserPassword = schoolManagementContext.Users.FirstOrDefault(x => x.UserId == user.UserId);
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
                SetUserPassword = schoolManagementContext.Users.FirstOrDefault(x => x.UserId == user.UserId);

                SetUserPassword.Password = EncryptionDecryption.Encrypt(user.Password.ToString());
                SetUserPassword.IsActive = true;
                SetUserPassword.ModifiedDate = DateTime.Now;

                schoolManagementContext.Entry(SetUserPassword).State = EntityState.Modified;
                schoolManagementContext.SaveChanges();

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
                GetResetPassword = schoolManagementContext.Users.FirstOrDefault(x => x.ResetPasswordCode == id);
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
                UpdateResetPassword = schoolManagementContext.Users.FirstOrDefault(x => x.ResetPasswordCode == model.ResetCode);

                UpdateResetPassword.Password = EncryptionDecryption.Encrypt(model.Password.ToString());

                schoolManagementContext.Entry(UpdateResetPassword).State = EntityState.Modified;
                schoolManagementContext.SaveChanges();

                return UpdateResetPassword;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
} 

