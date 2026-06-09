using BankApi.DTOs;
using BankApi.Interfaces;
using BankApi.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BankApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        private readonly IJwtService _jwtService;

        public AuthController(
            UserManager<User> userManager,
            IJwtService jwtService)
        {
            _userManager = userManager;

            _jwtService = jwtService;
        }

        // REGISTER
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            // Check existing email
            var existingUser =
                await _userManager.FindByEmailAsync(dto.Email);

            if (existingUser != null)
            {
                return BadRequest("Email already exists");
            }

            var user = new User
            {
                FullName = dto.FullName,

                UserName = dto.Email,

                Email = dto.Email
            };

            var result =
                await _userManager.CreateAsync(
                    user,
                    dto.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new
            {
                message = "User Registered Successfully"
            });
        }

        // LOGIN
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user =
                await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                return Unauthorized("Invalid Email");
            }

            var validPassword =
                await _userManager.CheckPasswordAsync(
                    user,
                    dto.Password);

            if (!validPassword)
            {
                return Unauthorized("Invalid Password");
            }

            var token =
                _jwtService.GenerateToken(user);

            return Ok(new
            {
                token
            });
        }
    }
}