using BlogAPI.Data;
using BlogAPI.DTOs.AccountDTOs;
using BlogAPI.Models;
using BlogAPI.Services.Token;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace BlogAPI.Repository.AuthRepo
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        public AuthRepository(UserManager<ApplicationUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<AuthDTO> LoginAsync(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
                return new AuthDTO { Message = "Invlaid email or password!" };

            var jwtSecurityToken = await _tokenService.CreateTokenAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            return new AuthDTO
            {
                IsAuthenticated = true,
                Email = user.Email,
                UserName = user.UserName,
                Roles = roles.ToList(),
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                ExpiresOn = jwtSecurityToken.ValidTo
            };
        }

        public async Task<AuthDTO> RegisterAsync(RegisterDTO dto)
        {
            if (await _userManager.FindByEmailAsync(dto.Email) != null)
                return new AuthDTO { Message = "Email already exists! " };

            if (await _userManager.FindByNameAsync(dto.Username) != null)
                return new AuthDTO { Message = "Username already exists!" };

            var user = new ApplicationUser
            {
                Firstname = dto.Firstname,
                Lastname = dto.Lastname,
                UserName = dto.Username,
                Email = dto.Email,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += $"{error.Description}, ";

                return new AuthDTO { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");

            var jwtSecurityToken = await _tokenService.CreateTokenAsync(user);

            return new AuthDTO
            {
                IsAuthenticated = true,
                Email = user.Email,
                UserName = user.UserName,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                ExpiresOn = jwtSecurityToken.ValidTo
            };
        }
    }
}
