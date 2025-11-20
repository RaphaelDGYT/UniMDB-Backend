using UniMDB.Domain.Entities;

namespace UniMDB.Application.Dtos;

public class UserFavoriteResponse
{
    public User UserFavorite { get; set; }

    public List<FavoriteResponseList> Favorites { get; set; }
}