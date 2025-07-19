using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Services.Implements
{
    public interface IUserGroupService
    {
        UserGroup? GetUserGroupById(int userGroupId);
        List<UserGroup>? GetUserGroups();
        List<UserGroup>? GetUserGroupsByUserId(int userId);
    }
}