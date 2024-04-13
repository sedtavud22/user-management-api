using UserManagement.API.Models.Domain;

namespace UserManagement.API.Repositories.Interface
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();
    }
}
