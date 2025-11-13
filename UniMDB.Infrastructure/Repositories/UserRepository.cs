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

    // CREATE
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
    public async Task<List<User>> AddBatchUserAsync(List<User> users)
    {
        try
        {
            _context.Users.AddRange(users);
            await _context.SaveChangesAsync();
            return users;
        }
        catch (Exception)
        {
            throw;
        }
    }

    // READ
    public async Task<List<uint>> GetAllUserIdsAsync()
    {
        return await _context.Users
                        .AsNoTracking()
                        .Select(u => u.id_user)
                        .ToListAsync()
                        ??
                        Enumerable.Empty<uint>().ToList();
    }
    public async Task<User> GetUserByIdAsync(uint id)
    {
        return await _context.Users.FindAsync(id);
    }
    public Task<User> GetUserBySessionAsync(User userSession)
    {

        try
        {
            return _context.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u =>
                            u.username == userSession.username &&
                            u.password == userSession.password &&
                            u.email == userSession.email
                        );
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
            return await _context.Reviews
                            .AsNoTracking()
                            .Where(r => r.id_review_user == id)
                            .ToListAsync()
                            ??
                            Enumerable.Empty<Review>().ToList();
        }
        catch (Exception)
        {
            throw; 
        }
    }
    
    // UPDATE
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
    public async Task<List<User>> UpdateBatchUserAsync(List<User> users)
    {
        try
        {
            _context.Users.UpdateRange(users);
            await _context.SaveChangesAsync();
            return users;
        }
        catch (Exception)
        {
            throw;
        }
    }

    // DELETE
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
}