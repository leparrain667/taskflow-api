using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.DTOs;

public class CreateProjectDto
{
    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }
}
