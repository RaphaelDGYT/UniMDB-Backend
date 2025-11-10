using System.Collections.Generic;
using System.Threading.Tasks;
using UniMDB.Domain.Entities;

namespace UniMDB.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User?> GetByIdAsync(uint id);
        Task<List<User>> GetAllAsync();
        Task<User?> UpdateAsync(User user);
        Task<bool> DeleteAsync(uint id);

        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByUsernameAsync(string username);

        Task<User?> GetUserByReviewAsync(uint reviewId);
    }
}