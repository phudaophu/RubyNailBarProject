using RubyNailBarWeb.Models;
using RubyNailBarWeb.Repositories;
using RubyNailBarWeb.Services.Implements;
using RubyNailBarWeb.StateStorage;
namespace RubyNailBarWeb.Services
{

    public class UsersService : IUsersService
    {
        private readonly UsersRepository usersRepository;

        public UsersService(UsersRepository _usersRepository)
        {
            this.usersRepository = _usersRepository;
        }


        public (int CurrentPage, int TotalPage) GetCurrentPageAndTotalPageService(int pageSize, int selectedUserId, int? selectedStoreId = null)
        {
            List<User> listUsers = new List<User> ();   
            if (selectedStoreId > 0)
            {
                listUsers = GetUsersService()
                                .Where(u => u.UserGroups != null && u.UserGroups
                                                                        .Any(ug => ug.StoreId != null && ug.StoreId == selectedStoreId))
                                                                        .ToList(); 
            }
            else
            {

               listUsers = GetUsersService();

            }

            int numberOfUsers = listUsers.Count;
           
            if(numberOfUsers <= 0) { return (0,0); }

            int totalPage = (int)Math.Ceiling((double)numberOfUsers / pageSize);

            int indexOfSelectedUserId = listUsers.FindIndex(x => x.UserId == selectedUserId) + 1;

            int currentPage = (int)Math.Ceiling((double)indexOfSelectedUserId / pageSize);

            return (currentPage, totalPage);

        }

        public List<User> GetStaffListByStoreIdService(int storeId)
        {
            return usersRepository.GetStaffListByStoreId(storeId);
        }

        public List<User> GetManagerListByStoreIdService(int storeId)
        {
            return usersRepository.GetManagerListByStoreId(storeId);    
        }


        // exclueedUserId is used to exclude the user from the check, useful when updating the user
        public bool IsUsernameExistsService(string username, int? excludedUserId = null)
        {
            return usersRepository.IsUsernameExists(username, excludedUserId);
        }

        public int AddUserService(User user)
        {
           return usersRepository.AddUser(user);
        }

        public void UpdateUserService(int userId, User user)
        {
            usersRepository.UpdateUser(userId, user);

        }

        public List<User> GetUsersService()
        {
            return usersRepository.GetUsers();
        }


        public User? GetUserByIdService(int userId)
        {
            var user = usersRepository.GetUserById(userId);
            if (user != null)
            {
                return user;
            }
            else
            {
                return new User();
            }

        }

        public List<User>? SearchUserService(string keyString)
        {
            return usersRepository.SearchUsers(keyString);
        }

    }
}