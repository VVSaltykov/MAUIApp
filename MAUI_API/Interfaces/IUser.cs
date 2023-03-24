using MAUIAppCommon.Models;

namespace MAUI_API.Interfaces
{
    public interface IUser
    {
        /// <summary>
        /// Get user by login
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        Task<User> GetUserByLoginAsync(string login);
        /// <summary>
        /// Get user by login
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        Task<User> GetUserByLoginAsync(LoginModel loginModel);
        /// <summary>
        /// Add user to database
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task AddNewUser(User user);
        /// <summary>
        /// Get user by login
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        Task<User> GetUserByLoginModelAsync(LoginModel loginModel);
        /// <summary>
        /// Checking if the user is in the database
        /// </summary>
        /// <param name="registerModel"></param>
        /// <returns></returns>
        Task<bool> UserIsInDatabase(RegisterModel registerModel);
        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        Task<User> GetUserByIdAsync(int userid);
    }
}
