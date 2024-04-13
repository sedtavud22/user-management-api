using Microsoft.EntityFrameworkCore;
using UserManagement.API.Data;
using UserManagement.API.Models.Domain;
using UserManagement.API.Repositories.Interface;

namespace UserManagement.API.Repositories.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        public UserRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<User> CreateAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<IEnumerable<User>> GetAllAsync(
            string? query = null,
            string? sortBy = null,
            string? sortDirection = null,
            int? pageNumber = 1,
            int? pageSize = 6)
        {

            Console.WriteLine(pageNumber);
            var users = dbContext.Users.Include(x => x.Permissions).ThenInclude(x => x.Permission).Include(x => x.Role).AsQueryable();

            // Filter
            if(string.IsNullOrWhiteSpace(query) == false)
            {
                users = users.Where(x => (x.FirstName.Contains(query) || x.LastName.Contains(query)));
            }

            // Sort
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(string.Equals(sortBy, "Name", StringComparison.OrdinalIgnoreCase))
                {
                    var isDesc = string.Equals(sortDirection,"desc",StringComparison.OrdinalIgnoreCase)
                        ? true : false;

                    users = isDesc ? users.OrderByDescending(x => x.FirstName) : users.OrderBy(x => x.FirstName);
                }

                if (string.Equals(sortBy, "Date", StringComparison.OrdinalIgnoreCase))
                {
                    var isDesc = string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase)
                        ? true : false;

                    users = isDesc ? users.OrderByDescending(x => x.CreatedAt) : users.OrderBy(x => x.CreatedAt);
                }

                if (string.Equals(sortBy, "Role", StringComparison.OrdinalIgnoreCase))
                {
                    var isDesc = string.Equals(sortDirection, "desc", StringComparison.OrdinalIgnoreCase)
                        ? true : false;

                    users = isDesc ? users.OrderByDescending(x => x.Role.RoleName) : users.OrderBy(x => x.Role.RoleName);
                }
            }

            // Pagination
            var skippedPage = (pageNumber - 1) * (pageSize ?? 6);
            users = users.Skip(skippedPage ?? 0).Take(pageSize ?? 6);

            return await users.ToListAsync();        
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await dbContext.Users.Include(x => x.Permissions).ThenInclude(x => x.Permission).Include(x => x.Role).FirstOrDefaultAsync(x => x.UserID == id);
        }

        public async Task<User?> UpdateAsync(string id,User user)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.UserID ==  id);

            if(existingUser == null)
            {
                return null;
            }

            var transaction = dbContext.Database.BeginTransaction();

            try
            {
                await dbContext.UserPermissions.Where(x => x.UserID == id).ExecuteDeleteAsync();
                dbContext.Entry(existingUser).CurrentValues.SetValues(user);
                foreach (var permission in user.Permissions)
                {
                    existingUser.Permissions.Add(permission);
                }
                await dbContext.SaveChangesAsync();
                transaction.Commit();
                return user;
            }
            catch
            {
                transaction.Rollback();
                return null;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public async Task<User?> DeleteAsync(string id)
        {
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(x => x.UserID == id);

            if (existingUser == null)
            {
                return null;
            }

            var transaction = dbContext.Database.BeginTransaction();

            try
            {
                dbContext.Users.Remove(existingUser);
                await dbContext.SaveChangesAsync();
                transaction.Commit();
                return existingUser;
            }
            catch
            {
                transaction.Rollback();
                return null;
            }
            finally
            {
                transaction.Dispose();
            }
        }

        public async Task<int> GetTotalCount()
        {
            return await dbContext.Users.CountAsync();
        }
    }
}
