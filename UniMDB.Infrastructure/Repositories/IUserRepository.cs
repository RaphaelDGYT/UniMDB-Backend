using UniMDB.Domain.Entities;

namespace UniMDB.Domain.Interfaces;

public interface IUserRepository
{
    Task<User> AddUserAsync(User user);
    Task<User> GetUserByIdAsync(uint id);
    Task<User> GetUserBySessionAsync(User user);
    Task<User> UpdateUserAsync(User user);
    Task<bool> DeleteUserAsync(uint id);
    Task<List<Review>> GetAllReviewsByUserIdAsync(uint id);

    //Task<User?> GetUserByReviewAsync(uint reviewId);
}