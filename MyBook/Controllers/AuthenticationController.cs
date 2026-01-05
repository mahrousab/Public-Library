using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PublicLibrary.Data.Models;
using PublicLibrary.DTOS;
using PublicLibrary.IRepositories;
using System.Security.Cryptography;

namespace PublicLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfigurationManager _configurationManager;
        private readonly AppDbContext _context;
        private readonly IJwtRepository _jwtRepository;
        public AuthenticationController(RoleManager<IdentityRole> roleManager, IConfigurationManager configurationManager, UserManager<ApplicationUser> userManager, AppDbContext context,IJwtRepository jwtRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configurationManager = configurationManager;
            _context = context;
            _jwtRepository = jwtRepository;

        }

        [HttpGet]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            var userExists = await _userManager.FindByNameAsync(model.UserName);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });
            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });
            return Ok(new { Status = "Success", Message = "User created successfully!" });

        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return Unauthorized();

            var roles = await _userManager.GetRolesAsync(user);
            var accessToken = _jwtRepository.GenerateToken(user, roles);

            var refreshToken = GenerateRefreshToken();

            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = refreshToken,
                UserId = user.Id,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            });

            await _context.SaveChangesAsync();

            return Ok(new
            {
                accessToken,
                refreshToken
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh(RefreshRequest dto)
        {
            var token = await _context.RefreshTokens
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Token == dto.RefreshToken);

            if (token == null || token.IsRevoked || token.ExpiresAt < DateTime.UtcNow)
                return Unauthorized();

            token.IsRevoked = true;

            var roles = await _userManager.GetRolesAsync(token.User);
            var newJwt = _jwtRepository.GenerateToken(token.User, roles);
            var newRefresh = GenerateRefreshToken();

            _context.RefreshTokens.Add(new RefreshToken
            {
                Token = newRefresh,
                UserId = token.UserId,
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            });

            await _context.SaveChangesAsync();

            return Ok(new
            {
                accessToken = newJwt,
                refreshToken = newRefresh
            });
        }
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout(RefreshRequest dto)
        {
            var token = await _context.RefreshTokens
                .FirstOrDefaultAsync(x => x.Token == dto.RefreshToken);

            if (token == null) return BadRequest();

            token.IsRevoked = true;
            await _context.SaveChangesAsync();

            return Ok();
        }


        private string GenerateRefreshToken()
        {
            return Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(64));
        }


    }
}