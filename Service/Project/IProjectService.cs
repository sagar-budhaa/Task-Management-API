using Task_Management_API.DTOs;
using Task_Management_API.Service.Result;

namespace Task_Management_API.Service.Project;

public interface IProjectService
{     public Task<ResultService<ProjectResponseDto>> CreateProjectAsync(ProjectCreateDto body, Guid userId);
     public Task<ResultService<List<ProjectResponseDto>>> GetAllProjectsAsync(Guid userId);
     public Task<ResultService<ProjectResponseDto>> GetProjectByIdAsync(Guid id, Guid userId);
     public Task<ResultService<ProjectResponseDto>> UpdateProjectAsync(Guid id, Guid userId, ProjectUpdateDto body);
     public Task<ResultService<bool>> DeleteProjectAsync(Guid id, Guid userId);
}