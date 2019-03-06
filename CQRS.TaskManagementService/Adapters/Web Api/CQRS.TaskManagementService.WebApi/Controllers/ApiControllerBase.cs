using Microsoft.AspNetCore.Mvc;

namespace CQRS.TaskManagementService.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
    }
}