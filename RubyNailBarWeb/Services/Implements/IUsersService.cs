using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Services.Implements
{
    public interface IUsersService
    {
        int AddUserService(User user);
        User? GetUserByIdService(int userId);
        List<User> GetUsersService();
        List<User>? SearchUserService(string keyString);
        void UpdateUserService(int userId, User user);
    }
}
