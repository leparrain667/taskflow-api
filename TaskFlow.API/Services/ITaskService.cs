using TaskFlow.API.DTOs;

namespace TaskFlow.API.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskDto>> GetAllAsync(int userId);
    Task<TaskDto?> GetByIdAsync(int id, int userId);
    Task<TaskDto> CreateAsync(CreateTaskDto dto, int userId);
    Task<TaskDto?> UpdateAsync(int id, UpdateTaskDto dto, int userId);
    Task<bool> DeleteAsync(int id, int userId);
}
