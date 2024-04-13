using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.API.Models.DTO;
using UserManagement.API.Models.Response;
using UserManagement.API.Repositories.Interface;

namespace UserManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly IPermissionRepository permissionRepository;
        public PermissionsController(IPermissionRepository permissionRepository)
        {
            this.permissionRepository = permissionRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            var result = await permissionRepository.GetAllAsync();
            var dataSource = new List<PermissionDto>();

            foreach (var permission in result)
            {
                dataSource.Add(new PermissionDto
                {
                    PermissionId = permission.PermissionId,
                    PermissionName = permission.PermissionName,
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
