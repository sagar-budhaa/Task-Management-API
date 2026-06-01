using Task_Management_API.DTOs;
using Task_Management_API.Service.Result;

namespace Task_Management_API.Service.Auth;

public interface IAuthService
{
    public Task<ResultService<UserRegisterResponsetDto>> RegisterUserAsync(UserRegisterRequestDto body);
    
    public Task<ResultService<UserLoginResponseDto>> LoginUserAsync(UserLoginRequestDto body);
}