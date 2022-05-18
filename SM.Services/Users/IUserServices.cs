using SM.Entity;
using SM.Models;
using System.Collections.Generic;

namespace SM.Services.Users
{
    public interface IUserServices
    {
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
