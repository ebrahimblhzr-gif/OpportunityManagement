using Microsoft.AspNetCore.Mvc;
using OpportunitiesBL.Interfaces;
using OpportunityAPI.DTOs;
using OpportunityEF.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace OpportunityAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AuthController(
            IUserService userService,
            IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO dto)
        {
            var existingUser =
                await _userService.GetByUsernameAsync(dto.Username);

            if (existingUser != null)
                return BadRequest("Username already exists.");

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };

            await _userService.AddAsync(user);

            return Ok("User registered successfully.");
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO dto)
        {
            var user =
                await _userService.GetByUsernameAsync(dto.Username);

            if (user == null)
                return Unauthorized("Invalid username or password.");

            bool isPasswordValid =
                BCrypt.Net.BCrypt.Verify(
                    dto.Password,
                    user.PasswordHash);

            if (!isPasswordValid)
                return Unauthorized("Invalid username or password.");

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.NameIdentifier,
            user.Id.ToString())
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    _configuration["Jwt:Key"]!));

            var credentials =
                new SigningCredentials(
                    key,
                    SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials);

            return Ok(new
            {
                Token = new JwtSecurityTokenHandler()
                    .WriteToken(token)
            });
        }


    }
}
