
using RubyNailBarWeb.Models;
using RubyNailBarWeb.Repositories;
using RubyNailBarWeb.Services.Implements;

namespace RubyNailBarWeb.Services
{
    public class UserGroupService : IUserGroupService
    {
        private readonly UserGroupRepository userGroupRepository;
        private readonly UsersRepository usersRepository;

        public UserGroupService(UserGroupRepository _userGroupRepository, UsersRepository usersRepository)
        {
            this.userGroupRepository = _userGroupRepository;
            this.usersRepository = usersRepository;
        }


        public List<UserGroup>? GetUserGroups()
        {
            var userGroups = userGroupRepository.GetUserGroups();
            if (userGroups is null)
            {
                return new List<UserGroup>();
            }
            return userGroups.OrderBy(ug => ug.UserGroupId).ToList();
        }

        public UserGroup? GetUserGroupById(int userGroupId)
        {
            var userGroup = userGroupRepository.GetUserGroupById(userGroupId);
            if (userGroup is null)
            {
                return new UserGroup();
            }
            return userGroup;

        }

        public List<UserGroup>? GetUserGroupsByUserId(int userId)
        {
            var userGroups = userGroupRepository.GetUserGroups();
            if (userGroups is null)
            {
                return new List<UserGroup>();
            }

            return userGroups.Where(ug => ug.User != null && ug.User.UserId == userId).OrderBy(ug => ug.UserGroupId).ToList();

        }

    }
}