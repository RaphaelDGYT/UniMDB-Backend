namespace UniMDB.Application.Dtos;

//  DTO usado para a API obter apenas as partes importantes para cadastrar um usuário

public class UserRegistration
{
    public string name { get; set; }
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }
}