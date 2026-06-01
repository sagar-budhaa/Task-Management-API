using Microsoft.EntityFrameworkCore;
using Task_Management_API.Data;
using Task_Management_API.DTOs;
using Task_Management_API.Service.Result;

namespace Task_Management_API.Service.Project;

using Models;
public class ProjectService(AppDbContext dbContext) : IProjectService
{
    public async Task<ResultService<ProjectResponseDto>> CreateProjectAsync(ProjectCreateDto body, Guid userId)
    {
        var project = new Project
        {
            Name = body.Name,
            Description = body.Description,
            Status = body.Status,
            UserId = userId
        };
        dbContext.Projects.Add(project);
        await dbContext.SaveChangesAsync();
        var dto = new ProjectResponseDto()
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            Status = project.Status!,
            CreatedAt = project.StartDate,
            IsActive = project.IsActive
        };
        return ResultService<ProjectResponseDto>.Created(dto);
    }

    public async Task<ResultService<List<ProjectResponseDto>>> GetAllProjectsAsync(Guid userId)
    {
        var projects = await dbContext.Projects.Where(p => p.UserId == userId).ToListAsync();
        var list = projects.Select(p => new ProjectResponseDto
        {
            Id = p.Id,
            Name = p.Name!,
            Description = p.Description!,
            Status = p.Status!,
            CreatedAt = p.StartDate,
            IsActive = p.IsActive
        }).ToList();
        return ResultService<List<ProjectResponseDto>>.Ok(list);
    }
    
    public async Task<ResultService<ProjectResponseDto>> GetProjectByIdAsync(Guid id, Guid userId)
    {
        var project = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        if (project == null) return ResultService<ProjectResponseDto>.NotFound("Project not found");
        var dto = new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name!,
            Description = project.Description!,
            Status = project.Status!,
            CreatedAt = project.StartDate,
            IsActive = project.IsActive
        };
        return ResultService<ProjectResponseDto>.Ok(dto);
    }

    public async Task<ResultService<ProjectResponseDto>> UpdateProjectAsync(Guid id, Guid userId, ProjectUpdateDto body)
    {
        var project = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        if (project == null) return ResultService<ProjectResponseDto>.NotFound("Project not found");
        project.Name = body.Name;
        project.Description = body.Description;
        project.Status = body.Status;
        project.IsActive = body.IsActive;
        await dbContext.SaveChangesAsync();
        var dto = new ProjectResponseDto
        {
            Id = project.Id,
            Name = project.Name!,
            Description = project.Description!,
            Status = project.Status!,
            CreatedAt = project.StartDate,
            IsActive = project.IsActive
        };
        return ResultService<ProjectResponseDto>.Ok(dto);
    }

    public async Task<ResultService<bool>> DeleteProjectAsync(Guid id, Guid userId)
    {
        var project = await dbContext.Projects.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
        if (project == null) return ResultService<bool>.NotFound("Project not found");
        dbContext.Projects.Remove(project);
        await dbContext.SaveChangesAsync();
        return ResultService<bool>.Ok(true);
    }
}