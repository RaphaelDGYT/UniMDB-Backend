namespace UniMDB.Application.Dtos;

//  DTO começam com maiuscula

public class ReviewCreation
{
    public byte Score { get; set; } = 0;
    public string Comment { get; set; } = string.Empty;
    public uint Id_User { get; set; } = 0;
    public string Id_Movie { get; set; } = string.Empty;
}