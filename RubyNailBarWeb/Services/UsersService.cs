using RubyNailBarWeb.Repositories;
using RubyNailBarWeb.Models;
using RubyNailBarWeb.Services.Implements;
namespace RubyNailBarWeb.Services
{

    public class UsersService : IUsersService
    {
        private readonly UsersRepository usersRepository;
        public UsersService(UsersRepository _usersRepository)
        {
            this.usersRepository = _usersRepository;
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