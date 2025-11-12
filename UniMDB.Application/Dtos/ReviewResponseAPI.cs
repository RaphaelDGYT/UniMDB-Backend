using UniMDB.Domain.Entities;

namespace UniMDB.Application.Dtos;

//  DTO começam com maiuscula

public class ReviewResponseAPI
{
    public uint Id { get; set; }
    public byte Score { get; set; }
    public string Comment { get; set; }
    public DateTime Created_at { get; set; }
    public User User { get; set; }
}