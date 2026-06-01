using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_Management_API.DTOs;
using Task_Management_API.Service.Auth;

namespace Task_Management_API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestDto body)
    {
        var result = await authService.RegisterUserAsync(body);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginUser([FromBody] UserLoginRequestDto body)
    {
        var result = await authService.LoginUserAsync(body);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("me")]
    [Authorize]
    public IActionResult GetMe()
    {
        var userName = User.Identity?.Name;
        return Ok(new
            {
                Username = userName,
            }
        );
    }
    
}