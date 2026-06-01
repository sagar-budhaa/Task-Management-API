using Task_Management_API.DTOs;
using Task_Management_API.Service.Result;

namespace Task_Management_API.Service.Task;

public interface ITaskService
{
    public Task<ResultService<List<TaskResponseDto>>> GetAllTasksAsync(Guid  projectId, Guid userId);
    public Task<ResultService<TaskResponseDto>> GetTaskByIdAsync(Guid id, Guid projectId, Guid userId);
    public Task<ResultService<TaskResponseDto>> CreateTaskAsync(TaskCreateDto body, Guid projectId, Guid userId);
    public Task<ResultService<TaskResponseDto>> UpdateTaskAsync(Guid id, Guid projectId, Guid userId, TaskUpdateDto body);
    public Task<ResultService<bool>> DeleteTaskAsync(Guid id, Guid projectId, Guid userId);
}