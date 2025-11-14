using UniMDB.Domain.Entities;

namespace UniMDB.Application.Dtos;

//  DTO começam com maiuscula

public class ReviewResponse
{
    public uint Id { get; set; } = 0;
    public uint User_Id { get; set; } = 0;
    public string Movie_Id { get; set; } = string.Empty;
    public byte Score { get; set; } = 0;
    public string Comment { get; set; } = string.Empty;
    public DateTime? Created_at { get; set; } = null;
}