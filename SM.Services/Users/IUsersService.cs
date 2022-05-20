using SM.Entity;
using SM.Models;
using System.Collections.Generic;

namespace SM.Services
{
    /// <summary>
    /// Interface for User services for crud operations.
    /// </summary>
    public interface IUsersService
    {
        User GetByEmail(string email);
        User GetById(int id);
        IEnumerable<User> GetAllUser();
        //User GetUser(User user);
        User Register(User user);
        //User GetUserPassword(User user);
        User SetUserPassword(User user);
        //User GetResetPassword(string id);
        //User ResetPassword(ForgotPassword model);
        //User ResetCode(string ResetCode);
    }
}
