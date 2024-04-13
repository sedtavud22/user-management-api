using System.ComponentModel.DataAnnotations;

namespace UserManagement.API.Models.Domain
{
    public class Permission
    {
        [Key] 
        public required string PermissionId { get; set; }

        public required string PermissionName { get; set; }

        public ICollection<UserPermission>? UserPermissions { get; set; }
    }
}
