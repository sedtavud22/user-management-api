using UserManagement.API.Models.Domain;

namespace UserManagement.API.Repositories.Interface
{
    public interface IPermissionRepository
    {
        Task<IEnumerable<Permission>> GetAllAsync();
    }
}
