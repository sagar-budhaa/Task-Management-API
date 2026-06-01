using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_Management_API.DTOs;
using Task_Management_API.Service.Project;

namespace Task_Management_API.Controllers.Project;

[ApiController]
[Route("api/[controller]")]
public class ProjectController(IProjectService projectService) : ControllerBase
{
    
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllProjects()
    {
        var userId = GetUserId();
        var result = await projectService.GetAllProjectsAsync(userId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetProjectByIdAsync([FromRoute] Guid id)
    {
        var userId = GetUserId();
        var result = await projectService.GetProjectByIdAsync(id, userId);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateProject([FromBody] ProjectCreateDto body)
    {
        var userId = GetUserId();
        var result = await projectService.CreateProjectAsync(body, userId);
        return StatusCode(result.StatusCode, result);
     }

    [HttpPut("{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateProject([FromRoute] Guid id, [FromBody] ProjectUpdateDto body)
    {
        var userId = GetUserId();
        var result = await projectService.UpdateProjectAsync(id, userId, body);
        return StatusCode(result.StatusCode, result);
    }

    [HttpDelete("{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteProject([FromRoute] Guid id)
    {
        var userId = GetUserId();
        var result = await projectService.DeleteProjectAsync(id, userId);
        return StatusCode(result.StatusCode, result);
    }
    
    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}