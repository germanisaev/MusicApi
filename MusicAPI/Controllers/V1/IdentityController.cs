using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicAPI.Contracts;
using MusicAPI.Helpers;
using MusicAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MusicAPI.Controllers.V1
{
    public class IdentityController: Controller
    {
        private readonly MusicApiContext _context;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;

        public IdentityController(MusicApiContext context, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        [HttpGet(ApiRoutes.Identity.GetBy)]
        public IActionResult GetById(int id)
        {
            var user = _context.UserDetails.Find(id);
            if(user == null)
            {
                return NotFound("User not found");
            }
            return Ok(user);
        }
        [HttpGet(ApiRoutes.Identity.GetAll)]
        public IActionResult GetAll()
        {
            var user = _context.UserDetails.ToList();
            if (user == null)
            {
                return NotFound("Users not found");
            }
            return Ok(user);
        }

        [HttpPost(ApiRoutes.Identity.Register)]
        public async Task<ActionResult<UserDetail>> PostIdentity([FromBody] UserDetail user)
        {
            user.Token = null;
            try
            {
                // save 
                //_userService.Create(user, userDto.Password);
                _context.UserDetails.Add(user);
                await _context.SaveChangesAsync();
                //return Ok();
                return CreatedAtAction("GetAll", new { id = user.Id }, user);
            }
            catch (AppException ex)
            {
                // return error message if there was an exception
                return BadRequest(new { message = ex.Message });
            }
            

            //return CreatedAtAction("GetAll", new { id = user.Id }, user);
        }

        [HttpPost(ApiRoutes.Identity.Login)]
        public async Task<ActionResult> PostLogin([FromBody] Identity login)
        {
            if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.Password))
                return null;
            // var result = _context.SongDetails.Where(x => x.Album == id).ToList();
            var user = _context.UserDetails.Where(x => x.UserName == login.UserName && x.Password == login.Password).FirstOrDefault();

            // check if username exists
            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            // check if password is correct
            //var pass = _context.UserDetails.SingleOrDefault(x => x.Password == login.Password);
            //if (pass == null)
            //    return null;
            //if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
            //    return null;
            //user.Token = CreateToken(user);
            //_context.Entry(user).State = EntityState.Modified;
            //await _context.SaveChangesAsync();
               
            return Ok(new
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Token = CreateToken(user)
            });
        }

        private string CreateToken(UserDetail user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
