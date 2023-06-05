using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService, IMapper mapper)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _mapper = mapper;
            _tokenService = tokenService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username)) return BadRequest("Username is Taken");

            var user = _mapper.Map<AppUser>(registerDto);

            // using statements ensure that classes that implement the IDisposable interface call their dispose method. 
            // It guarantees that the dispose method will be called, even if the code throws an exception.
            //using var hmac = new HMACSHA512(); 

            user.UserName = registerDto.Username.ToLower();

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
            //return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid username");

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded)   return Unauthorized();

            // getting passwordSalt of existing user in db 
            //using var hmac = new HMACSHA512(user.PasswordSalt);

            // computing current login password hash
            // var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password)); 

            // for(int i = 0; i < computedHash.Length; i++){
            //     // comparing password of db user and login user, if not match then returning unauthorized
            //     if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            //}

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs,
                Gender = user.Gender
            };
            // return user;
        }

        private async Task<bool> UserExists(string username)
        {
            // returning matched user from db
            return await _userManager.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}