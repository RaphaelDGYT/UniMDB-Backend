namespace UniMDB.Application.Dtos;

//  DTO começam com maiuscula

public class UserResponseAPI
{
    public uint Id { get; set; } = 0;
    public string Name { get; set; } = String.Empty;
    public string Username { get; set; } = String.Empty;
    public string Email { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public List<uint> Ids_reviews { get; set; }
}
