using UniMDB.Domain.Entities;

namespace UniMDB.Application.Dtos;

public class UserReviewsResponse
{
    public User UserAutor { get; set; }

    public List<ReviewResponseList> Reviews { get; set; }
}
