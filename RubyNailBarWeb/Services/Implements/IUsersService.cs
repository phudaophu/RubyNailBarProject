using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Services.Implements
{
    public interface IUsersService
    {
        List<User> GetValidUsersOrderByUserIdDescService();
        (int CurrentPage, int TotalPage) GetCurrentPageAndTotalPageService(int pageSize, int selectedUserId, int? selectedStoreId = null);
        List<User> GetStaffListByStoreIdService(int storeId);
        List<User> GetManagerListByStoreIdService(int storeId);  
        bool IsUsernameExistsService(string username, int? excludedUserId = null); 
        int AddUserService(User user);  
        User? GetUserByIdService(int userId);
        List<User> GetUsersService();
        List<User>? SearchUserService(string keyString);
        void UpdateUserService(int userId, User user);
    }
}
