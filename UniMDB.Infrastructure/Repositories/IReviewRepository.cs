using UniMDB.Domain.Entities;

namespace UniMDB.Domain.Interfaces;

public interface IReviewRepository
{
    // CREATE
    Task<Review> AddReviewAsync(Review review);
    Task<List<Review>> AddBatchReviewAsync(List<Review> reviews);

    // READ
    Task<Review?> GetReviewByIdAsync(uint id);
    //Task<List<Review>> GetAllReviewsByUserIdAsync(uint id_user);

    // UPDATE
    Task<Review?> UpdateReviewAsync(uint id, Review reviewNova);
    
    // DELETE
    Task<bool> DeleteReviewAsync(uint id);

}