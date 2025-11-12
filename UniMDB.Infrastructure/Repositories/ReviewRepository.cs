using UniMDB.Infrastructure.Data;
using UniMDB.Domain.Interfaces;
using UniMDB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace UniMDB.Infrastructure.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly ApplicationDbContext _context;
    public ReviewRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Review> AddReviewAsync(Review review)
    {
        try
        {
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return review;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> DeleteReviewAsync(uint id)
    {
        try
        {
            var review = await _context.Reviews.FindAsync(id);

            if (review == null)
                return false;

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<Review>> GetAllReviewsByUser(uint userId)
    {
        try
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return null;
            }

            return await _context.Reviews.Where(r => r.id_review_user == userId).ToListAsync();
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<Review> GetReviewByIdAsync(uint id)
    {
        return await _context.Reviews.FindAsync(id);
    }

    public async Task<Review> UpdateReviewAsync(Review review)
    {
        try
        {
            _context.Reviews.Update(review);
            await _context.SaveChangesAsync();
            return review;
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<User> GetUserByReviewAsync(uint reviewId)
    {
        try
        {
            var review = await _context.Reviews.FindAsync(reviewId);

            if (review == null)
            {
                return null;
            }

            return await _context.Users.FindAsync(review.id_review_user);

        }
        catch (Exception)
        {
            throw;
        }

    }
}