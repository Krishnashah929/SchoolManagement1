using SM.Entity;
using SM.Models;
using System.Collections.Generic;

namespace SM.Services.Users
{
    /// <summary>
    /// Interface for User services for crud operations.
    /// </summary>
    public interface IUserServices
    {
        IEnumerable<User> GetAll();
        User GetByEmail(User user);
        User Register(User user);
        User GetUserPassword(User user);
        User SetUserPassword(User user);
        User GetResetPassword(string id);
        User ResetPassword(ForgotPassword model);
    }
}
