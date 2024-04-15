using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using UserManagement.API.Data;
using UserManagement.API.Models.Domain;
using UserManagement.API.Models.DTO;
using UserManagement.API.Models.Response;
using UserManagement.API.Repositories.Interface;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository userRepository;
        public UsersController(IUserRepository userRepository) 
        {
            this.userRepository = userRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserDto request)
        {


            var user = new User
            {
                UserID = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                RoleID = request.RoleID,
                Username = request.Username,
                Password = request.Password,
            };

            

            if (request.Permissions.Length > 0 )
            {
                var perms = request.Permissions.Select(x => new UserPermission()
                {
                    UserID = request.Id,
                    IsDeletable = x.IsDeletable,
                    IsReadable = x.IsReadable,
                    IsWriteable = x.IsReadable,
                    PermissionID = x.PermissionID,
                }).ToList();

                var filteredPerms = perms.Where(x => (x.IsDeletable || x.IsReadable || x.IsWriteable) == true).ToList();

                user.Permissions = filteredPerms;
            }
           

            await userRepository.CreateAsync(user);

            var result = await userRepository.GetByIdAsync(request.Id);

            var data = new UserDto
            {
                UserID = result.UserID,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                Phone = result.Phone ?? null,
                Username = result.Username,
                Role = new RoleDto
                {
                    RoleId = result.Role.RoleId,
                    RoleName = result.Role.RoleName
                },
                Permissions = result.Permissions.Select(x => new PermissionDto
                {
                    PermissionId = x.Permission.PermissionId,
                    PermissionName = x.Permission.PermissionName
                }).ToList(),
                CreatedAt = result.CreatedAt,
                UpdatedAt = result.UpdatedAt
            };

            var response = new ResponseWrapper
            {
                Status = new ResponseWrapperStatus
                {
                    Code = "201",
                    Description = "Success"
                },
                Data = data,
            };


            return Ok(response);
        }

        [HttpGet]
        [Route("datatable")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? query,
            [FromQuery] string? sortBy,
            [FromQuery] string? sortDirection,
            [FromQuery] int? pageNumber,
            [FromQuery] int? pageSize)
        {
            var result = await userRepository.GetAllAsync(query, sortBy, sortDirection, pageNumber, pageSize);
            var dataSource = new List<UserDto>();

            foreach (var user in result)
            {
                dataSource.Add(new UserDto
                {
                    UserID = user.UserID,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone ?? null,
                    Username = user.Username,
                    Role = new RoleDto
                    {
                        RoleId = user.Role.RoleId,
                        RoleName = user.Role.RoleName
                    },
                    Permissions = user.Permissions.Select(x => new PermissionDto
                    {
                        PermissionId = x.Permission.PermissionId,
                        PermissionName = x.Permission.PermissionName
                    }).ToList(),
                    CreatedAt = user.CreatedAt,
                    UpdatedAt = user.UpdatedAt              
                }); 
            }

            var totalCount = await userRepository.GetTotalCount();

            var response = new GetAllResponse
            {
                Data = dataSource,
                Page = pageNumber ?? 1,
                PageSize = dataSource.Count(),
                TotalCount = totalCount,
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] string id)
        {
            var result = await userRepository.GetByIdAsync(id);

            if(result == null)
            {
                return BadRequest("User not found");
            }

            var data = new UserDto
            {
                UserID = result.UserID,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                Phone = result.Phone ?? null,
                Username = result.Username,
                Role = new RoleDto
                {
                    RoleId = result.Role.RoleId,
                    RoleName = result.Role.RoleName
                },
                Permissions = result.Permissions.Select(x => new PermissionDto
                {
                    PermissionId = x.Permission.PermissionId,
                    PermissionName = x.Permission.PermissionName
                }).ToList(),
                CreatedAt = result.CreatedAt,
                UpdatedAt = result.UpdatedAt
            };

            var response = new ResponseWrapper
            {
                Status = new ResponseWrapperStatus
                {
                    Code = "200",
                    Description = "Success"
                },
                Data = data,
            };

            return Ok(response);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> EditUser([FromRoute] string id,EditUserDto request)
        {
            var user = new User
            {
                UserID = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone,
                RoleID = request.RoleID,
                Username = request.Username,
                Password = request.Password,
            };



            if (request.Permissions.Length > 0)
            {
                var perms = request.Permissions.Select(x => new UserPermission()
                {
                    UserID = id,
                    IsDeletable = x.IsDeletable,
                    IsReadable = x.IsReadable,
                    IsWriteable = x.IsReadable,
                    PermissionID = x.PermissionID,
                }).ToList();

                var filteredPerms = perms.Where(x => (x.IsDeletable || x.IsReadable || x.IsWriteable) == true).ToList();

                user.Permissions = filteredPerms;
            }

            var updatedUser = await userRepository.UpdateAsync(id, user);

            if(updatedUser == null)
            {
                return BadRequest("Error");
            }

            var result = await userRepository.GetByIdAsync(id);

            if (result == null)
            {
                return BadRequest("User not found");
            }

            var data = new UserDto
            {
                UserID = result.UserID,
                FirstName = result.FirstName,
                LastName = result.LastName,
                Email = result.Email,
                Phone = result.Phone ?? null,
                Username = result.Username,
                Role = new RoleDto
                {
                    RoleId = result.Role.RoleId,
                    RoleName = result.Role.RoleName
                },
                Permissions = result.Permissions.Select(x => new PermissionDto
                {
                    PermissionId = x.Permission.PermissionId,
                    PermissionName = x.Permission.PermissionName
                }).ToList(),
                CreatedAt = result.CreatedAt,
                UpdatedAt = result.UpdatedAt
            };

            var response = new ResponseWrapper
            {
                Status = new ResponseWrapperStatus
                {
                    Code = "200",
                    Description = "Success"
                },
                Data = data,
            };

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            await userRepository.DeleteAsync(id);

            var response = new ResponseWrapper
            {
                Status = new ResponseWrapperStatus
                {
                    Code = "204",
                    Description = "Success"
                },
                Data = new DeleteResponse
                {
                    Result = true,
                    Message = $"User {id} deleted"
                }
            };

            return Ok(response);
        }
    }
}
