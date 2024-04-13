namespace UserManagement.API.Models.DTO
{
    public class EditUserDto
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public string? Phone { get; set; }

        public required string RoleID { get; set; }
        public required string Username { get; set; }

        public required string Password { get; set; }

        public DateTime UpdatedAt {  get; set; } = DateTime.UtcNow;

        public CreateUserPermission[] Permissions { get; set; } = [];
    }
}
