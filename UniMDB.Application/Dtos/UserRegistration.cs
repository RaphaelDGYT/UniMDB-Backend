namespace UniMDB.Application.Dtos;

//  DTO usado para a API obter apenas as partes importantes para cadastrar um usuário

public class UserRegistration
{
    public string Name { get; set; }  = string.Empty;
    public string Username { get; set; }  = string.Empty;
    public string Email { get; set; }  = string.Empty;
    public string Password { get; set; }  = string.Empty;
}