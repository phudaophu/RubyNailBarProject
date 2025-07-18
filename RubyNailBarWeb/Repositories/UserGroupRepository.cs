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
            return db.UserGroups.ToList();
        }

    }
}
