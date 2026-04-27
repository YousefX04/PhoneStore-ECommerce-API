using PhoneStore.Application.DTOs.Authentication;

namespace PhoneStore.Application.Services.Interfaces
{
    public interface IAuthService
    {
        Task Register(UserRegisterDto model);
        Task<AuthDto> Login(UserLoginDto model);
    }
}
