using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;

namespace UniMDB.Application.Services;

public interface IReviewService
{
    // CREATE
    Task<ReviewResponse> AddReview(ReviewCreation review);
    Task<List<ReviewResponse>> AddBatchReview(List<ReviewCreation> reviews);

    // READ
    Task<ReviewResponse> GetReviewById(uint id_review);

    // UPDATE
    Task<ReviewResponse> UpdateReview(uint id_review, ReviewUpdate review);
    
    // DELETE
    Task<bool> DeleteReview(uint id_review);
}