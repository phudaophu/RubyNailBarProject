using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Services.Implements
{
    public interface IUserGroupService
    {
        int GetMaxUserGroupId();
        UserGroup? GetUserGroupById(int userGroupId);
        List<UserGroup>? GetUserGroups();
        List<UserGroup>? GetUserGroupsByUserId(int userId);
        void UpdateUserGroup(int userGroupId,UserGroup userGroup);
        void AddUserGroup(UserGroup userGroup);
        void RemoveUserGroup(int userGroupId);
    }
}