using Microsoft.EntityFrameworkCore;
using UserManagement.API.Data;
using UserManagement.API.Models.Domain;
using UserManagement.API.Repositories.Interface;

namespace UserManagement.API.Repositories.Implementation
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationDbContext dbContext;
        public RoleRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await dbContext.Roles.ToListAsync();
        }
    }
}
