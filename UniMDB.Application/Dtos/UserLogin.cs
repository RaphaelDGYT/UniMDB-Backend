
namespace UniMDB.Application.Dtos;

//  DTO começam com maiuscula

public class UserLogin
{
    public string Username { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
}
