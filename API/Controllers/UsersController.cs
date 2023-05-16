using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //[ApiController] // [ApiController] attribute can be applied to a controller class to enable API-specific behaviors
    //[Route("api/[controller]")] // Route Specifies URL pattern for a controller or action
    public class UsersController : BaseApiController
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        // https://localhost:5000/api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers(){
            // ActionResult method works as a return type of any controller method in the MVC.
            return await _context.Users.ToListAsync();
        }

        // https://localhost:5000/api/users/1
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser(int id){
            // ActionResult method works as a return type of any controller method in the MVC.
            return await _context.Users.FindAsync(id);
        }
    }
}