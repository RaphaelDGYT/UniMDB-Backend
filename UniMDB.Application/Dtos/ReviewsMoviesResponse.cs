using UniMDB.Domain.Entities;

namespace UniMDB.Application.Dtos;

public class ReviewsMoviesResponse
{
    public string Movies_Id { get; set; }

    public List<ReviewResponseList> Reviews { get; set; }
}