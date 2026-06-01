using Microsoft.EntityFrameworkCore;
using Task_Management_API.Data;
using Task_Management_API.DTOs;
using Task_Management_API.lib;
using Task_Management_API.Models;
using Task_Management_API.Service.Result;

namespace Task_Management_API.Service.Auth;

public class AuthService(AppDbContext dbContext, JwtService jwtService) : IAuthService
{
    
    public async Task<ResultService<UserRegisterResponsetDto>> RegisterUserAsync(UserRegisterRequestDto body)
    {
        if (body.Username == "" || body.Password == "") return ResultService<UserRegisterResponsetDto>.BadRequest("Username and password are required");
        var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == body.Username);
        if (existingUser != null)
            return ResultService<UserRegisterResponsetDto>.BadRequest("User already exists");
        var user = new User
        {
            Username = body.Username,
            Password = body.Password
        };
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();
        var dto = new UserRegisterResponsetDto
        {
            Success = true,
            Message = "User registered successfully"
        };
        return ResultService<UserRegisterResponsetDto>.Created(dto);
    }

    public async Task<ResultService<UserLoginResponseDto>> LoginUserAsync(UserLoginRequestDto body)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(
            u => u.Username == body.Username && u.Password == body.Password);
        if (user == null)
        {
            return ResultService<UserLoginResponseDto>.BadRequest("Invalid username or password");
        }
        var accessToken = jwtService.GenerateToken(user);
        var dto = new UserLoginResponseDto()
        {
            Success = true,
            AccessToken = accessToken,
            Message = "User logged in"
        };
        return ResultService<UserLoginResponseDto>.Ok(dto);
    }
}