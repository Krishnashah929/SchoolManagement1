using SM.Entity;
using SM.Repositories.IRepository;
using System;
using System.Collections.Generic;
using SM.Web.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore;

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
        public User GetById(User user)
        {
            try
            {
                User getUser = new User();
                getUser = _schoolManagementContext.Users.FirstOrDefault(x => x.EmailAddress == user.EmailAddress);
                if (getUser != null)
                {
                    //have to remove this.
                    getUser.EmailAddress = user.EmailAddress;

                    _schoolManagementContext.SaveChanges();
                }
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
                if (_schoolManagementContext.Users.Where(x => x.EmailAddress == user.EmailAddress).Count() == 0)
                {
                    registerUser.Password = string.Empty;
                    registerUser.CreatedDate = DateTime.Now;
                    registerUser.IsActive = false;

                    _schoolManagementContext.Users.Add(registerUser);
                    _schoolManagementContext.SaveChanges();
                }
                return registerUser;
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
    }
}
