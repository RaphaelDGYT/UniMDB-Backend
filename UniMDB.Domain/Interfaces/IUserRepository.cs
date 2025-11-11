using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using UniMDB.Domain.Entities;

namespace UniMDB.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> CreateAsync(User user);
        Task<User> GetByUserAsync(string email, string passworld);
        Task<User?> GetByIdAsync(uint id);
        Task<User?> UpdateAsync(User user);
        Task<bool> DeleteAsync(uint id);
        
    }
}