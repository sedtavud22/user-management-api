using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.API.Models.DTO;
using UserManagement.API.Models.Response;
using UserManagement.API.Repositories.Interface;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository roleRepository;
        public RolesController(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await roleRepository.GetAllAsync();
            var dataSource = new List<RoleDto>();

            foreach (var role in result)
            {
                dataSource.Add(new RoleDto
                {
                    RoleId = role.RoleId,
                    RoleName = role.RoleName,
                });
            }

            var response = new ResponseWrapper
            {
                Status = new ResponseWrapperStatus
                {
                    Code = "200",
                    Description = "Success"
                },
                Data = dataSource
            };
            return Ok(response);
        }
    }
}
