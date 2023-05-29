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
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;
        public AccountController(DataContext context, ITokenService tokenService, IMapper mapper)
        {
            _mapper = mapper;
            _tokenService = tokenService;
            _context = context;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto){
            if(await UserExists(registerDto.Username)) return BadRequest("Username is Taken");

            var user = _mapper.Map<AppUser>(registerDto);

            // using statements ensure that classes that implement the IDisposable interface call their dispose method. 
            // It guarantees that the dispose method will be called, even if the code throws an exception.
            using var hmac = new HMACSHA512(); 

            // var user = new AppUser
            // {
            user.UserName = registerDto.Username.ToLower();
            user.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password));
            user.PasswordSalt = hmac.Key;
            // };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                KnownAs = user.KnownAs
            };
            //return user;
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto){
            var user = await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == loginDto.Username );

            if(user == null) return Unauthorized("Invalid username");

            // getting passwordSalt of existing user in db 
            using var hmac = new HMACSHA512(user.PasswordSalt);

            // computing current login password hash
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password)); 

            for(int i = 0; i < computedHash.Length; i++){
                // comparing password of db user and login user, if not match then returning unauthorized
                if(computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateToken(user),
                PhotoUrl = user.Photos.FirstOrDefault(x => x.IsMain)?.Url,
                KnownAs = user.KnownAs
            };
            // return user;
        }

        private async Task<bool> UserExists(string username){
            // returning matched user from db
            return await _context.Users.AnyAsync(x => x.UserName == username.ToLower());
        }
    }
}