using Microsoft.EntityFrameworkCore;
using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Repositories
{
    public class UserGroupRepository
    {
        private readonly IDbContextFactory<NailsDbContext> contextFactory;
        public UserGroupRepository(IDbContextFactory<NailsDbContext> _contextFactory)
        {
            this.contextFactory = _contextFactory;

        }

        public List<UserGroup>? GetUserGroups()
        {
            using var db = this.contextFactory.CreateDbContext();
            return db.UserGroups.Include(ug => ug.Store).Include(ug => ug.User).ToList();
        }

        public UserGroup? GetUserGroupById(int userGroupId)
        {
            using var db = this.contextFactory.CreateDbContext();
            return db.UserGroups.Find(userGroupId);
        }

        public void UpdateUserGroup(int userGroupId,UserGroup userGroup)
        {
            using var db = this.contextFactory.CreateDbContext();
            var userGroupToUpdate = db.UserGroups.Find(userGroupId);
            if (userGroupToUpdate != null)
            {
                userGroupToUpdate.StoreId = userGroup.StoreId;
                userGroupToUpdate.RoleName = userGroup.RoleName;
                userGroupToUpdate.GroupName = userGroup.GroupName;
            }
            db.SaveChanges();
        }

        public void AddUserGroup(UserGroup userGroup)
        {
            using var db = this.contextFactory.CreateDbContext();
            db.UserGroups.Add(userGroup);
            db.SaveChanges();
        }
        public void RemoveUserGroup(int userGroupId)
        {
            using var db = this.contextFactory.CreateDbContext();
            var userGroup = db.UserGroups.Find(userGroupId);
            if (userGroup != null)
            {
                db.UserGroups.Remove(userGroup);
                db.SaveChanges();
            }

        }
    }
}
