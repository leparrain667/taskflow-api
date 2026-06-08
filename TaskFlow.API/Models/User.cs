using System.ComponentModel.DataAnnotations;

namespace TaskFlow.API.Models;

/// <summary>
/// Utilisateur de l'application TaskFlow.
/// </summary>
public class User
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public string PasswordHash { get; set; } = string.Empty;

    [Required]
    public UserRole Role { get; set; } = UserRole.User;

    /// <summary>
    /// Liste des projets appartenant à cet utilisateur.
    /// </summary>
    public ICollection<Project> Projects { get; set; } = new List<Project>();
}
