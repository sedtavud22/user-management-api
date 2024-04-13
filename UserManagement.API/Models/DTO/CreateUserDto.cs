namespace UserManagement.API.Models.DTO
{
    public class CreateUserDto
    {
        public required string Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }

        public required string RoleID { get; set; }
        public required string Username { get; set; }

        public required string Password { get; set; }

        public required CreateUserPermission[] Permissions { get; set; }
    }

    public class CreateUserPermission
    {
        public required string PermissionID { get; set; }
        public bool IsReadable { get; set; }
        public bool IsWritable { get; set; }
        public bool IsDeletable { get; set; }
    }
}
