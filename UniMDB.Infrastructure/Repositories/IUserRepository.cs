using UniMDB.Domain.Entities;

namespace UniMDB.Domain.Interfaces;

public interface IUserRepository
{
    // CREATE
    Task<User> AddUserAsync(User user);
    Task<List<User>> AddBatchUserAsync(List<User> users);

    // READ
    Task<List<uint>> GetAllUserIdsAsync();
    Task<User> GetUserByIdAsync(uint id);
    Task<User> GetUserBySessionAsync(User user);
    Task<List<Review>> GetAllReviewsByUserIdAsync(uint id);

    //Task<User> GetUserByReview(uint reviewId);


    // UPDATE
    Task<User> UpdateUserAsync(User user);
    Task<List<User>> UpdateBatchUserAsync(List<User> users);

    // DELETE
    Task<bool> DeleteUserAsync(uint id);
}