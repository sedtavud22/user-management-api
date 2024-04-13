using UserManagement.API.Models.Domain;

namespace UserManagement.API.Models.DTO
{
    public class UserDto
    {
        public required string UserID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        public required string Username { get; set; }

        public required DateTime CreatedAt { get; set; }
        public required DateTime UpdatedAt { get; set; } 

        public required RoleDto Role { get; set; }
        public required List<PermissionDto> Permissions { get; set; }
    }
}
