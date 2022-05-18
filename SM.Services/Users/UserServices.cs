using SM.Entity;
using SM.Models;
using SM.Repositories.IRepository;
using System;
using System.Collections.Generic;

namespace SM.Services.Users
{
    public class UserServices : IUserServices
    {
        private readonly IUserRepository _userRepository;

        public UserServices(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User Delete(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetByEmail(User user)
        {
            return _userRepository.GetByEmail(user);
        }

        public User GetById(User user)
        {
            throw new NotImplementedException();
        }

        public User Register(User user)
        {
            return _userRepository.Register(user);
        }

        public User Update(User user)
        {
            throw new NotImplementedException();
        }
        public User GetUserPassword(User user)
        {
            return _userRepository.GetUserPassword(user);
        }
         public User SetUserPassword(User user)
        {
            return _userRepository.SetUserPassword(user);
        }
         public User GetResetPassword(string id)
        {
            return _userRepository.GetResetPassword(id);
        }
         public User ResetPassword(ForgotPassword model)
        {
            return _userRepository.ResetPassword(model);
        }

    }
}
