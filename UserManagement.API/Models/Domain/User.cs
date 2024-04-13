using System.ComponentModel.DataAnnotations;
using System.Data;

namespace UserManagement.API.Models.Domain
{
    public class User
    {
        [Key]
        public required string UserID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }
        
        public required string RoleID { get; set; }
        public required string Username { get; set; }

        public required string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public Role? Role { get; set; }
        public ICollection<UserPermission> Permissions { get; set; } = [];
    }
}
