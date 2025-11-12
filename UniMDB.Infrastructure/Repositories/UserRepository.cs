using UniMDB.Infrastructure.Data;
using UniMDB.Domain.Interfaces;
using UniMDB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace UniMDB.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<User> AddUserAsync(User user)
    {
        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<User> GetUserByIdAsync(uint id)
    {
        return await _context.Users.FindAsync(id);
    }
    public Task<User> GetUserBySessionAsync(User userSession)
    {

        try
        {
            var user = _context.Users.FirstOrDefaultAsync(u =>
                u.username == userSession.username &&
                u.password == userSession.password &&
                u.email == userSession.email
            );

            return user;
        }
        catch (Exception)
        {
            throw;
        }

    }
    public async Task<User> UpdateUserAsync(User user)
    {
        try
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<bool> DeleteUserAsync(uint id)
    {

        try
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
        
    }
    public async Task<List<Review>> GetAllReviewsByUserIdAsync(uint id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            return await _context.Reviews.Where(r => r.id_review_user == id).ToListAsync();
        }
        catch (Exception)
        {
            throw; 
        }
    }

    /*
        public Task<User?> GetUserByReviewAsync(uint reviewId)
        {
           // TODO: Implementar busca por review (se existir relação)
           throw new NotImplementedException();
        }
    */

}