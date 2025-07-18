using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using RubyNailBarWeb.Models;

namespace RubyNailBarWeb.Repositories
{
    public class UsersRepository
    {
        private readonly IDbContextFactory<NailsDbContext> contextFactory;
        public UsersRepository(IDbContextFactory<NailsDbContext> _contextFactory) 
        {
            this.contextFactory = _contextFactory;  
        
        }

        public void AddUser (User user) 
        { 
            using var db = this.contextFactory.CreateDbContext();
            db.Users.Add(user);
            db.SaveChanges();
        
        }
        public List<User> GetUsers()
        {
            using var db = this.contextFactory.CreateDbContext();
            return db.Users.Include(user => user.UserGroups).ToList();

        }

        public User? GetUserById(int userId) 
        {
            using var db = this.contextFactory.CreateDbContext();
            var user = db.Users.Find(userId);
            if (user is not null)
            { 
                return user;
            }
            else
            {
                return new User();

            }
        }

        public void UpdateUser(int userId, User user)
        {
            if (user is null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            if (userId != user.UserId) return;

            using var db = this.contextFactory.CreateDbContext();
            var userToUpdate = db.Users.Find(userId);
            if (userToUpdate != null)
            {
                //userToUpdate.UserId = user.UserId;
                userToUpdate.Username = user.Username;
                userToUpdate.PasswordHash = user.PasswordHash;
                userToUpdate.FirstName = user.FirstName;
                userToUpdate.LastName = user.LastName;
                userToUpdate.Email = user.Email;
                userToUpdate.PhoneNo = user.PhoneNo;
                userToUpdate.IsActive = user.IsActive;
                userToUpdate.ModifiedDatetime = DateTime.Now;
                db.SaveChanges();
            }
        }

        public List<User>? SearchUsers(string keyString)
        {
            using var db = this.contextFactory.CreateDbContext();
            var userList = db.Users.Where(x =>
                                        (x.Username != null &&
                                         x.Username.ToLower().IndexOf(keyString.ToLower()) >= 0)  ||
                                        (x.FirstName != null &&
                                         x.FirstName.ToLower().IndexOf(keyString.ToLower()) >= 0) ||
                                        (x.LastName != null &&
                                         x.LastName.ToLower().IndexOf(keyString.ToLower()) >= 0)  ||
                                        (x.Email != null &&
                                         x.Email.ToLower().IndexOf(keyString.ToLower()) >= 0)     ||
                                        (x.PhoneNo != null &&
                                         x.PhoneNo.ToLower().IndexOf(keyString.ToLower()) >= 0)
                                                                                                        ).Include(user => user.UserGroups).ToList();
            return userList;
        }


    }
}
