using FluentValidation;
using Microsoft.AspNetCore.Identity;
using PhoneStore.Application.DTOs.Authentication;
using PhoneStore.Application.ExternalServices;
using PhoneStore.Application.Services.Interfaces;
using PhoneStore.Domain.Entities;
using PhoneStore.Domain.Repositories.Interfaces;

namespace PhoneStore.Application.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UserLoginDto> _loginValidator;
        private readonly IValidator<UserRegisterDto> _regitserValidator;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JwtService _jwtService;

        public AuthService(IUnitOfWork unitOfWork, IValidator<UserRegisterDto> regitserValidator, UserManager<ApplicationUser> userManager, JwtService jwtService, IValidator<UserLoginDto> loginValidator)
        {
            _unitOfWork = unitOfWork;
            _regitserValidator = regitserValidator;
            _userManager = userManager;
            _jwtService = jwtService;
            _loginValidator = loginValidator;
        }

        public async Task<AuthDto> Login(UserLoginDto model)
        {
            var result = _loginValidator.Validate(model);

            if (!result.IsValid)
                throw new Exception(result.ToString(","));

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user == null)
                throw new Exception("Email or password is incorrect!");

            var isAuthenticated = await _userManager.CheckPasswordAsync(user, model.Password);

            if (!isAuthenticated)
                throw new Exception("Email or password is incorrect!");

            var userRole = await _userManager.GetRolesAsync(user);

            var token = _jwtService.GenerateToken(
                new UserDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    Role = userRole.First()
                });

            var authDto = new AuthDto
            {
                Token = token
            };

            return authDto;
        }

        public async Task Register(UserRegisterDto model)
        {
            var result = _regitserValidator.Validate(model);

            if (!result.IsValid)
                throw new Exception(result.ToString(","));

            var user = new ApplicationUser
            {
                UserName = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Gender = model.Gender,
                DateOfBirth = model.DateOfBirth,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address,
                EmailConfirmed = true
            };

            var creationResult = await _userManager.CreateAsync(user, model.Password);

            if (!creationResult.Succeeded)
                throw new Exception(string.Join(",", creationResult.Errors.Select(e => e.Description)));

            var roleResult = await _userManager.AddToRoleAsync(user, "User");

            if (!roleResult.Succeeded)
                throw new Exception(string.Join(",", roleResult.Errors.Select(e => e.Description)));

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
