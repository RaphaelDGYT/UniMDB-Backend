namespace UniMDB.Application.Dtos;

//  DTO usado pela API para ela enviar apenas as partes importantes de um usuário

public class UserResponseAPI
{
    public uint id { get; set; }
    public string name { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public uint total_reviews { get; set; }
    public List<uint> ids_reviews { get; set; }
}
