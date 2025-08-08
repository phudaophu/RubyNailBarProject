
using Microsoft.Identity.Client;
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

        public int GetMaxUserGroupId()
        {
            var userGroups = userGroupRepository.GetUserGroups();
            if (userGroups is null || !userGroups.Any())
            {
                return 0;
            }
            return userGroups.Max(ug => ug.UserGroupId);
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

        public void UpdateUserGroup(int userGroupId ,UserGroup userGroup)
        {
            if (userGroup is null)
            {
                throw new ArgumentNullException(nameof(userGroup), "User group cannot be null");
            }
            var existingUserGroup = userGroupRepository.GetUserGroupById(userGroupId);
            if (existingUserGroup is null)
            {
                throw new KeyNotFoundException($"User group with ID {userGroupId} not found.");
            }
            else if ( userGroup.GroupName != existingUserGroup.GroupName || userGroup.RoleName != existingUserGroup.RoleName || userGroup.StoreId != existingUserGroup.StoreId )
            {
                userGroupRepository.UpdateUserGroup(userGroupId,userGroup);
            }
            else
            {
                 return;
            }
        }

        public void AddUserGroup(UserGroup userGroup)
        {
            if (userGroup is null)
            {
                throw new ArgumentNullException(nameof(userGroup), "User group cannot be null");
            }
            //var existingUserGroup = userGroupRepository.GetUserGroups();
            //if (existingUserGroup != null && existingUserGroup.Any(ug => ug.GroupName == userGroup.GroupName && ug.RoleName == userGroup.RoleName && ug.StoreId == userGroup.StoreId))
            //{
            //    throw new InvalidOperationException("A user group with the same name, role, and store already exists.");
            //}
            userGroupRepository.AddUserGroup(userGroup);
        }

        public void RemoveUserGroup(int userGroupId)
        {
            if (userGroupId <= 0)
            {
                throw new ArgumentException("Invalid user group ID.", nameof(userGroupId));
            }
            userGroupRepository.RemoveUserGroup(userGroupId);
        }

    }
}