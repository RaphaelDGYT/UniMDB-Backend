using UniMDB.Application.Dtos;
using UniMDB.Infrastructure.Data;

namespace UniMDB.Application.Services;

//  Aqui vai ficar todas as implementações que envolvam o banco de dados e os usuários

//  TODO: Fazer as implementações   xD

public class ReviewService : IReviewService
{
    private readonly ApplicationDbContext _appDbContext;
    public ReviewService(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }


    public Task<ReviewCreation> AddReview(ReviewCreation review)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteReview(uint id_review)
    {
        throw new NotImplementedException();
    }

    public Task<List<ReviewResponse>> GetAllReviewsByUser(uint id_user)
    {
        throw new NotImplementedException();
    }

    public Task<ReviewResponse> GetReviewById(uint id_review)
    {
        throw new NotImplementedException();
    }

    public Task<ReviewResponse> UpdateReview(uint id_review, ReviewCreation review)
    {
        throw new NotImplementedException();
    }
}