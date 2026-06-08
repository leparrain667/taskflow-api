using System.ComponentModel.DataAnnotations;
using TaskFlow.API.Models;

namespace TaskFlow.API.DTOs;

public class CreateTaskDto
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    public TaskItemStatus Status { get; set; } = TaskItemStatus.AFaire;

    public DateTime? DueDate { get; set; }

    [Required]
    public int ProjectId { get; set; }

    public List<string> Commentaires { get; set; } = new();
}
