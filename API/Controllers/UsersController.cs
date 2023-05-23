using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //[ApiController] // [ApiController] attribute can be applied to a controller class to enable API-specific behaviors
    //[Route("api/[controller]")] // Route Specifies URL pattern for a controller or action
    [Authorize]
    public class UsersController : BaseApiController
    {
        // private readonly DataContext _context;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            // _context = context;
        }

        // https://localhost:5000/api/users
        // [AllowAnonymous] // don't ask the users to be authenticated whenever they want to access this ActionResult.
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers(){
            // ActionResult method works as a return type of any controller method in the MVC.
            // return await _context.Users.ToListAsync();

            var users = await _userRepository.GetMembersAsync();

            return Ok(users);
        }

        // https://localhost:5000/api/users/1
        // [Authorize]
        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username){
            // ActionResult method works as a return type of any controller method in the MVC.
            return await _userRepository.GetMemberAsync(username);
        }
    }
}