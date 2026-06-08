using TaskFlow.API.Models;

namespace TaskFlow.API.DTOs;

public class TaskDto
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public TaskItemStatus Status { get; set; }
    public DateTime? DueDate { get; set; }
    public int ProjectId { get; set; }
    public List<string> Commentaires { get; set; } = new();
}
