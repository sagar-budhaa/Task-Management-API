using Microsoft.EntityFrameworkCore;
using Task_Management_API.Data;
using Task_Management_API.DTOs;
using Task_Management_API.Service.Result;

namespace Task_Management_API.Service.Task;

public class TaskService(AppDbContext dbContext) : ITaskService
{
    public async Task<ResultService<List<TaskResponseDto>>> GetAllTasksAsync(Guid projectId, Guid userId)
    {
        var project = await dbContext.Projects.FindAsync(projectId);
        if (project == null || project.UserId != userId)
        {
            return ResultService<List<TaskResponseDto>>.NotFound("Project not found or access denied");
        }

        var tasks = await dbContext.Tasks.Where(t => t.ProjectId == projectId).ToListAsync();
        var list = tasks.Select(t => new TaskResponseDto
        {
            Id = t.Id,
            Name = t.Name,
            ProjectId = t.ProjectId,
            IsCompleted = t.IsActive,
            CreatedAt = t.created_at,
            UpdatedAt = t.updated_at

        }).ToList();
        return ResultService<List<TaskResponseDto>>.Ok(list);
    }

    public async Task<ResultService<TaskResponseDto>> GetTaskByIdAsync(Guid id, Guid projectId, Guid userId)
    {
        var project = await dbContext.Projects.FindAsync(projectId);
        if (project == null || project.UserId != userId)
        {
            return ResultService<TaskResponseDto>.NotFound("Project not found or access denied");
        }

        var tasks = await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.ProjectId == projectId);
        if (tasks == null)
        {
            return ResultService<TaskResponseDto>.NotFound("Task not found");
        }

        var dto = new TaskResponseDto
        {
            Id = tasks.Id,
            Name = tasks.Name,
            ProjectId = tasks.ProjectId,
            IsCompleted = tasks.IsActive,
            CreatedAt = tasks.created_at,
            UpdatedAt = tasks.updated_at
        };
        return ResultService<TaskResponseDto>.Ok(dto);
    }

    public async Task<ResultService<TaskResponseDto>> CreateTaskAsync(TaskCreateDto body, Guid projectId, Guid userId)
    {
        var project = await dbContext.Projects.FindAsync(projectId);
        if (project == null || project.UserId != userId)
        {
            return ResultService<TaskResponseDto>.NotFound("Project not found or access denied");
        }

        var task = new Models.Task
        {
            Name = body.Name!,
            ProjectId = projectId,
            IsActive = body.IsActive
        };
        dbContext.Tasks.Add(task);
        await dbContext.SaveChangesAsync();
        var dto = new TaskResponseDto
        {
            Id = task.Id,
            Name = task.Name,
            ProjectId = task.ProjectId,
            IsCompleted = task.IsActive,
            CreatedAt = task.created_at,
            UpdatedAt = task.updated_at
        };
        return ResultService<TaskResponseDto>.Created(dto);
    }

    public async Task<ResultService<TaskResponseDto>> UpdateTaskAsync(Guid id, Guid projectId, Guid userId, TaskUpdateDto body)
    {
        var project = await dbContext.Projects.FindAsync(projectId);
        if (project == null || project.UserId != userId)
        {
            return ResultService<TaskResponseDto>.NotFound("Project not found or access denied");
        }
        var task = await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.ProjectId == projectId);
        if (task == null)
        {
            return ResultService<TaskResponseDto>.NotFound("Task not found");
        }
        task.Name = body.Name!;
        task.IsActive = body.IsActive;
        task.updated_at = DateTime.UtcNow;
        await dbContext.SaveChangesAsync();
        var dto = new TaskResponseDto        {
            Id = task.Id,
            Name = task.Name,
            ProjectId = task.ProjectId,
            IsCompleted = task.IsActive,
            CreatedAt = task.created_at,
            UpdatedAt = task.updated_at
        };
        return ResultService<TaskResponseDto>.Ok(dto);
        
    }

    public async Task<ResultService<bool>> DeleteTaskAsync(Guid id, Guid projectId, Guid userId)
    {
        var project = await dbContext.Projects.FindAsync(projectId);
        if (project == null || project.UserId != userId)
        {
            return ResultService<bool>.NotFound("Project not found or access denied");
        }

        var task = await dbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.ProjectId == projectId);
        if (task == null)
        {
            return ResultService<bool>.NotFound("Task not found");
        }
        dbContext.Tasks.Remove(task);
        await dbContext.SaveChangesAsync();
        return ResultService<bool>.Ok(true);
    }
}