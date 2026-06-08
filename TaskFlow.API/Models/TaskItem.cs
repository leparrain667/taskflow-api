using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.Models;

/// <summary>
/// Tâche appartenant à un projet.
/// (Renommée TaskItem pour éviter le conflit avec System.Threading.Tasks.Task.)
/// </summary>
public class TaskItem
{
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public TaskItemStatus Status { get; set; } = TaskItemStatus.AFaire;

    public DateTime? DueDate { get; set; }

    // Clé étrangère vers Project
    public int ProjectId { get; set; }
    public Project? Project { get; set; }

    /// <summary>
    /// Collection de commentaires / notes de suivi.
    /// </summary>
    public List<string> Commentaires { get; set; } = new List<string>();
}
