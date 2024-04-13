using Microsoft.EntityFrameworkCore;
using UserManagement.API.Data;
using UserManagement.API.Models.Domain;
using UserManagement.API.Repositories.Interface;

namespace UserManagement.API.Repositories.Implementation
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationDbContext dbContext;
        public PermissionRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<Permission>> GetAllAsync()
        {
            return await dbContext.Permissions.ToListAsync();
        }
    }
}
