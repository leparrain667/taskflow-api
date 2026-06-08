using TaskFlow.API.DTOs;

namespace TaskFlow.API.Services;

public interface IProjectService
{
    Task<IEnumerable<ProjectDto>> GetAllAsync(int userId);
    Task<ProjectDto?> GetByIdAsync(int id, int userId);
    Task<ProjectDto> CreateAsync(CreateProjectDto dto, int userId);
    Task<ProjectDto?> UpdateAsync(int id, UpdateProjectDto dto, int userId);
    Task<bool> DeleteAsync(int id, int userId);
}
