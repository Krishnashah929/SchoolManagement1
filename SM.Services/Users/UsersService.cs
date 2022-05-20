using Microsoft.EntityFrameworkCore;
using SM.Common;
using SM.Entity;
using SM.Repository.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SM.Services
{
    /// <summary>
    /// User services for crud operations
    /// </summary>
    public class UsersService : IUsersService
    {
        #region Fields
        /// <summary>
        /// The unit of work.
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        #region Ctor
        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The Unit Of Work.</param>
        public UsersService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #endregion

        public IEnumerable<User> GetAllUser()
        {
            return this.GetAll();
        }
        private List<User> GetAll()
        {
            try
            {
                var repoList = this._unitOfWork.GetRepository<User>();
                List<User> lstUsers = repoList.GetAll().AsNoTracking().ToList();

                return lstUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public User GetById(int id)
        {
            try
            {
                return this.GetAll().FirstOrDefault(x => x.UserId == id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public User GetByEmail(string email)
        {
            try
            {
                return this.GetAll().FirstOrDefault(x => x.EmailAddress.ToLower() == email.ToLower());
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
                if (user != null)
                {
                    var userRepository = _unitOfWork.GetRepository<User>();
                    if (userRepository == null)
                    {
                        user.Password = string.Empty;
                        user.CreatedDate = DateTime.Now;
                        user.IsActive = false;
                        user.Role = "Admin";

                        userRepository.Add(user);
                        
                        _unitOfWork.Commit();
                    }
                }
                return user;
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
                var userRepository = _unitOfWork.GetRepository<User>();
                User SetUserPassword = new User();
                SetUserPassword = this.GetAll().FirstOrDefault(x => x.UserId == user.UserId);

                SetUserPassword.Password = EncryptionDecryption.Encrypt(user.Password.ToString());
                SetUserPassword.IsActive = true;
                SetUserPassword.ModifiedDate = DateTime.Now;

                userRepository.Update(SetUserPassword);
                //_unitOfWork.Users.Update(user);
                //_schoolManagementContext.Entry(UpdateResetPassword).State = EntityState.Modified;
                _unitOfWork.Commit();

                return SetUserPassword;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //getbyid
        //public User GetUserPassword(User user)
        //{
        //    try
        //    {
        //        User GetUserPassword = new User();
        //        GetUserPassword = this.GetAll().FirstOrDefault(x => x.UserId == user.UserId);
        //        return GetUserPassword;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public User ResetCode(string ResetCode)
        //{
        //    try
        //    {
        //        User user = new User();
        //        user.ResetPasswordCode = ResetCode;
        //        _unitOfWork.Commit();
        //        return user;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public User GetResetPassword(string id)
        //{
        //    try
        //    {
        //        User GetResetPassword = new User();
        //        GetResetPassword = this.GetAll().FirstOrDefault(x => x.ResetPasswordCode == id);
        //        return GetResetPassword;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public User ResetPassword(ForgotPassword model)
        //{
        //    try
        //    {
        //        User UpdateResetPassword = new User();
        //        UpdateResetPassword = this.GetAll().FirstOrDefault(x => x.ResetPasswordCode == model.ResetCode);

        //        UpdateResetPassword.Password = EncryptionDecryption.Encrypt(model.Password.ToString());

        //        //_schoolManagementContext.Entry(UpdateResetPassword).State = EntityState.Modified;
        //        _unitOfWork.Commit();

        //        return UpdateResetPassword;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

    }
}

