using System.ComponentModel.DataAnnotations;

namespace UserManagement.API.Models.Domain
{
    public class Role
    {
        [Key]
        public required string RoleId { get; set; } 
        public required string RoleName { get; set; }

        public ICollection<User>? Users { get; set; }
    }
}
