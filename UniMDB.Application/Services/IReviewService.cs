using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;

namespace UniMDB.Application.Services;

public interface IReviewService
{
    // CREATE
    Task<ReviewResponseAPI> AddReview(ReviewCreation review);
    Task<List<ReviewResponseAPI>> AddBatchReview(List<ReviewCreation> reviews);

    // READ
    Task<ReviewResponseAPI> GetReviewById(uint id_review);
    Task<List<ReviewResponseAPI>> GetAllReviewsByUserId(uint id);

    // UPDATE
    Task<ReviewResponseAPI> UpdateReview(uint id_review, ReviewCreation review);
    
    // DELETE
    Task<bool> DeleteReview(uint id_review);
}