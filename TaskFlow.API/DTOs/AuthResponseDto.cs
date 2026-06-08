namespace TaskFlow.API.DTOs;

/// <summary>
/// Réponse retournée après un register/login : le token JWT et quelques infos utilisateur.
/// </summary>
public class AuthResponseDto
{
    public string Token { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}
