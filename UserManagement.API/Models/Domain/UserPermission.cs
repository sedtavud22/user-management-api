using System.ComponentModel.DataAnnotations;

namespace UserManagement.API.Models.Domain
{
    public class UserPermission
    {
        [Key]
        public int? ID { get; set; }
        public required string UserID { get; set; }
        public required string PermissionID { get; set; }

        public bool IsReadable { get; set; } = false;
        public bool IsWriteable { get; set; } = false;
        public bool IsDeletable { get; set; } = false;

        public User? User { get; set; }
        public Permission? Permission { get; set; }
    }
}
