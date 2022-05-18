using SM.Entity;
using SM.Models;
using SM.Repository.Core.Uow;
using System;
using System.Collections.Generic;

namespace SM.Services.Users
{
    /// <summary>
    /// User services for crud operations
    /// </summary>
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<User> GetAll()
        {
            return _unitOfWork.GetAll();
        }
        public User GetByEmail(User user)
        {
            return _unitOfWork.GetByEmail(user);
        }
       public User GetUser(User user)
        {
            return _unitOfWork.GetUser(user);
        }
        public User Register(User user)
        {
            return _unitOfWork.Register(user);
        }
        public User GetUserPassword(User user)
        {
            return _unitOfWork.GetUserPassword(user);
        }
        public User SetUserPassword(User user)
        {
            return _unitOfWork.SetUserPassword(user);
        }
        public User GetResetPassword(string id)
        {
            return _unitOfWork.GetResetPassword(id);
        }
        public User ResetPassword(ForgotPassword model)
        {
            return _unitOfWork.ResetPassword(model);
        }
        public User ResetCode(string ResetCode)
        {
            return _unitOfWork.ResetCode(ResetCode);
        }

        //public User GetByEmail(User user)
        //{
        //    var userRepository = _unitOfWork.GetRepository<User>();
        //    userRepository.Add(user);
        //    _unitOfWork.Commit();
        //    return _unitOfWork.GetByEmail(user);
        //}
    }
}

