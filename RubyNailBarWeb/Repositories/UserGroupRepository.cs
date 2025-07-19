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

    }
}
