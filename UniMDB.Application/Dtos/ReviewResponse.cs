namespace UniMDB.Application.Dtos;

//  DTO começam com maiuscula

public class ReviewResponse
{
    public string User { get; set; } = string.Empty;
    public byte Score { get; set; } = 0;
    public string Comment { get; set; } = String.Empty;
    public DateTime Created_at { get; set; }
}