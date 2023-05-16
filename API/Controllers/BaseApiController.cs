using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController] // [ApiController] attribute can be applied to a controller class to enable API-specific behaviors
    [Route("api/[controller]")] // Route Specifies URL pattern for a controller or action
    public class BaseApiController : ControllerBase
    {
        
    }
}