namespace TaskFlow.API.Exceptions;

/// <summary>
/// Levée quand un utilisateur authentifié tente d'accéder à une ressource qui ne lui appartient pas (HTTP 403).
/// </summary>
public class ForbiddenException : Exception
{
    public ForbiddenException(string message) : base(message) { }
}
