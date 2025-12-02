using UniMDB.Domain.Entities;

namespace UniMDB.Application.Dtos;

//  DTO começam com maiuscula

public class FavoriteResponseList
{
    public uint Id_Favorite { get; set; } = 0;
    public uint Id_User { get; set; } = 0;
    public string Id_Movie_Mdb { get; set; } = string.Empty;

}