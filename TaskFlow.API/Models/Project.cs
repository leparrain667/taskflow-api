using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.Models;

/// <summary>
/// Projet contenant une liste de tâches, appartenant à un utilisateur.
/// </summary>
public class Project
{
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(500)]
    public string? Description { get; set; }

    public DateTime CreationDate { get; set; } = DateTime.UtcNow;

    // Clé étrangère vers User
    public int UserId { get; set; }
    public User? User { get; set; }

    /// <summary>
    /// Liste des tâches associées à ce projet.
    /// </summary>
    public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
}
