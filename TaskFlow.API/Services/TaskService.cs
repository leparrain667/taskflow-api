using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data;
using TaskFlow.API.DTOs;
using TaskFlow.API.Exceptions;
using TaskFlow.API.Models;

namespace TaskFlow.API.Services;

public class TaskService : ITaskService
{
    private readonly ApplicationDbContext _context;

    public TaskService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskDto>> GetAllAsync(int userId)
    {
        return await _context.Tasks
            .Include(t => t.Project)
            .Where(t => t.Project!.UserId == userId)
            .Select(t => new TaskDto
            {
                Id = t.Id,
                Title = t.Title,
                Status = t.Status,
                DueDate = t.DueDate,
                ProjectId = t.ProjectId,
                Commentaires = t.Commentaires
            })
            .ToListAsync();
    }

    public async Task<TaskDto?> GetByIdAsync(int id, int userId)
    {
        var task = await _context.Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null || task.Project!.UserId != userId) return null;

        return Map(task);
    }

    public async Task<TaskDto> CreateAsync(CreateTaskDto dto, int userId)
    {
        var project = await _context.Projects.FindAsync(dto.ProjectId);
        if (project == null)
            throw new KeyNotFoundException("Projet introuvable.");
        if (project.UserId != userId)
            throw new ForbiddenException("Ce projet ne vous appartient pas.");

        var task = new TaskItem
        {
            Title = dto.Title,
            Status = dto.Status,
            DueDate = dto.DueDate,
            ProjectId = dto.ProjectId,
            Commentaires = dto.Commentaires
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return Map(task);
    }

    public async Task<TaskDto?> UpdateAsync(int id, UpdateTaskDto dto, int userId)
    {
        var task = await _context.Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null) return null;
        if (task.Project!.UserId != userId)
            throw new ForbiddenException("Cette tâche ne vous appartient pas.");

        task.Title = dto.Title;
        task.Status = dto.Status;
        task.DueDate = dto.DueDate;
        task.Commentaires = dto.Commentaires;

        await _context.SaveChangesAsync();
        return Map(task);
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var task = await _context.Tasks
            .Include(t => t.Project)
            .FirstOrDefaultAsync(t => t.Id == id);

        if (task == null) return false;
        if (task.Project!.UserId != userId)
            throw new ForbiddenException("Cette tâche ne vous appartient pas.");

        _context.Tasks.Remove(task);
        await _context.SaveChangesAsync();
        return true;
    }

    private static TaskDto Map(TaskItem t) => new()
    {
        Id = t.Id,
        Title = t.Title,
        Status = t.Status,
        DueDate = t.DueDate,
        ProjectId = t.ProjectId,
        Commentaires = t.Commentaires
    };
}
