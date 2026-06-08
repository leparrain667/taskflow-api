using Microsoft.EntityFrameworkCore;
using TaskFlow.API.Data;
using TaskFlow.API.DTOs;
using TaskFlow.API.Exceptions;
using TaskFlow.API.Models;

namespace TaskFlow.API.Services;

public class ProjectService : IProjectService
{
    private readonly ApplicationDbContext _context;

    public ProjectService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProjectDto>> GetAllAsync(int userId)
    {
        return await _context.Projects
            .Where(p => p.UserId == userId)
            .Select(p => new ProjectDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CreationDate = p.CreationDate,
                UserId = p.UserId
            })
            .ToListAsync();
    }

    public async Task<ProjectDto?> GetByIdAsync(int id, int userId)
    {
        var p = await _context.Projects.FindAsync(id);
        if (p == null || p.UserId != userId) return null;

        return new ProjectDto
        {
            Id = p.Id,
            Name = p.Name,
            Description = p.Description,
            CreationDate = p.CreationDate,
            UserId = p.UserId
        };
    }

    public async Task<ProjectDto> CreateAsync(CreateProjectDto dto, int userId)
    {
        var project = new Project
        {
            Name = dto.Name,
            Description = dto.Description,
            UserId = userId,
            CreationDate = DateTime.UtcNow
        };

        _context.Projects.Add(project);
        await _context.SaveChangesAsync();

        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreationDate = project.CreationDate,
            UserId = project.UserId
        };
    }

    public async Task<ProjectDto?> UpdateAsync(int id, UpdateProjectDto dto, int userId)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return null;
        if (project.UserId != userId)
            throw new ForbiddenException("Vous n'êtes pas le propriétaire de ce projet.");

        project.Name = dto.Name;
        project.Description = dto.Description;
        await _context.SaveChangesAsync();

        return new ProjectDto
        {
            Id = project.Id,
            Name = project.Name,
            Description = project.Description,
            CreationDate = project.CreationDate,
            UserId = project.UserId
        };
    }

    public async Task<bool> DeleteAsync(int id, int userId)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null) return false;
        if (project.UserId != userId)
            throw new ForbiddenException("Vous n'êtes pas le propriétaire de ce projet.");

        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
        return true;
    }
}
