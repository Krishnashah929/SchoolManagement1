using SM.Entity;
using SM.Repositories.IRepository;
using System;
using System.Collections.Generic;
using SM.Web.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SM.Common;
using SM.Models;

namespace SM.Repositories.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly SchoolManagementContext _schoolManagementContext;

        public UserRepository(SchoolManagementContext schoolManagementContext)
        {
            _schoolManagementContext = schoolManagementContext;
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
        public User GetById(User user)
        {
            try
            {
                User getUser = new User();
                getUser = _schoolManagementContext.Users.FirstOrDefault(x => x.UserId == user.UserId);
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
        public User Delete(User user)
        {
            try
            {
                User deleteUser = new User();
                deleteUser = _schoolManagementContext.Users.FirstOrDefault(x => x.UserId == user.UserId);

                if (deleteUser != null)
                {
                    if (deleteUser.IsDelete == false)
                    {
                        deleteUser.IsDelete = true;
                        deleteUser.IsActive = false;
                    }

                    _schoolManagementContext.SaveChanges();
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

                updateUser = _schoolManagementContext.Users.FirstOrDefault(x => x.UserId == user.UserId);

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

                    _schoolManagementContext.Entry(updateUser).State = EntityState.Modified;

                    _schoolManagementContext.SaveChanges();
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
    }
}
