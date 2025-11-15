namespace UniMDB.Application.Dtos;

//  DTO começam com maiuscula

public class UserResponse
{
    public uint Id { get; set; } = 0;
    public string Name { get; set; } = String.Empty;
    public string Username { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Token { get; set; } = String.Empty;
    // a ideia é gerar um token de acesso ou validação para o front
    // e apartir dele gerar uma session
    
}