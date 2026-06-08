using System.ComponentModel.DataAnnotations;
using TaskFlow.API.Models;

namespace TaskFlow.API.DTOs;

public class UpdateTaskDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public TaskItemStatus Status { get; set; }

    public DateTime? DueDate { get; set; }

    public List<string> Commentaires { get; set; } = new();
}
