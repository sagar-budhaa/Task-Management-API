using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Task_Management_API.DTOs;
using Task_Management_API.Service.Task;

namespace Task_Management_API.Controllers.Task;

[ApiController]
[Route("api/[controller]")]
public class TaskController(ITaskService taskService) : ControllerBase
{
    
    [HttpGet("{projectId}")]
    [Authorize]
    public async Task<IActionResult> GetAllTasks([FromRoute] Guid projectId)
    {
        var userId =  GetUserId();
        var result = await taskService.GetAllTasksAsync(projectId, userId);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpGet("{projectId}/{id}")]
    [Authorize]
    public async Task<IActionResult> GetTaskById([FromRoute]Guid id, [FromRoute]Guid projectId)
    {
        var userId = GetUserId();
        var result = await taskService.GetTaskByIdAsync(id, projectId, userId);
        return StatusCode(result.StatusCode, result);
    }

    [HttpPost("{projectId}")]
    [Authorize]
    public async Task<IActionResult> CreateTaskAsync([FromBody] TaskCreateDto body, [FromRoute] Guid projectId)
    {
        var userId =  GetUserId();
        var result = await taskService.CreateTaskAsync(body, projectId, userId);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpPut("{projectId}/{id}")]
    [Authorize]
    public async Task<IActionResult> UpdateTaskAsync([FromRoute] Guid id, [FromBody] TaskUpdateDto body, [FromRoute] Guid projectId)
    {
        var userId = GetUserId();
        var result = await taskService.UpdateTaskAsync(id, projectId, userId, body);
        return StatusCode(result.StatusCode, result);
    }
    
    [HttpDelete("{projectId}/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteTaskAsync([FromRoute] Guid id, [FromRoute] Guid projectId)
    {
        var userId = GetUserId();
        var result = await taskService.DeleteTaskAsync(id, projectId, userId);
        return StatusCode(result.StatusCode, result);
    }
    
    
    
    private Guid GetUserId()
    {
        return Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}