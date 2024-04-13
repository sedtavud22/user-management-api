using UserManagement.API.Models.Domain;

namespace UserManagement.API.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<IEnumerable<User>> GetAllAsync(
            string? query = null,
            string? sortBy = null,
            string? sortDirection = null,
            int? pageNumber = 1,
            int? pageSize = 6);
        Task<User?> GetByIdAsync(string id);
        Task<User?> UpdateAsync(string id, User user);
        Task<User?> DeleteAsync(string id);

        Task<int> GetTotalCount();
    }
}
