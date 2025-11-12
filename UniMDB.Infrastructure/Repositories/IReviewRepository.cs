using UniMDB.Domain.Entities;

namespace UniMDB.Domain.Interfaces;

public interface IReviewRepository
{
    Task<Review> AddReviewAsync(Review review);
    Task<Review> GetReviewByIdAsync(uint id);
    Task<List<Review>> GetAllReviewsByUser(uint userId);
    Task<Review> UpdateReviewAsync(Review review);
    Task<bool> DeleteReviewAsync(uint id);

    //Task<User?> GetUserByReviewAsync(uint reviewId);
}