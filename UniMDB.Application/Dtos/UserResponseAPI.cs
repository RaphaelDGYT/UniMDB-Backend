namespace UniMDB.Application.Dtos;

//  DTO usado pela API para ela enviar apenas as partes importantes de um usuário

public class UserResponseAPI
{
    public uint Id { get; set; }
    public string Name { get; set; }  = string.Empty;
    public string Username { get; set; }  = string.Empty;
    public string Email { get; set; }  = string.Empty;
    public string Password { get; set; }  = string.Empty;
    public uint Total_reviews { get; set; }  
    public List<uint> ids_reviews { get; set; }
}