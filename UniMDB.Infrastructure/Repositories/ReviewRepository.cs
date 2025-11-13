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

    // CREATE
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
    public async Task<List<Review>> AddBatchReviewAsync(List<Review> reviews)
    {
        try
        {
            _context.Reviews.AddRange(reviews);
            await _context.SaveChangesAsync();
            return reviews;
        }
        catch (Exception)
        {
            throw;
        }
    }
    
    // READ
    public async Task<Review> GetReviewByIdAsync(uint id)
    {
        return await _context.Reviews.FindAsync(id);
    }

    public async Task<List<Review>> GetAllReviewsByUserIdAsync(uint id_user)
    {
        try
        {
            return await _context.Reviews
                            .AsNoTracking()
                            .Where(r => r.id_review_user == id_user)
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
    public async Task<Review> UpdateReviewAsync(uint id, Review reviewNova)
    {
        try
        {
            var reviewAchada = await _context.Reviews.FindAsync(id);

            if (reviewAchada == null)
            {
                return null;
            }

            reviewNova.id_review = reviewAchada.id_review;
            reviewNova.id_review_user = reviewAchada.id_review_user;
            reviewAchada.review = reviewNova.review;
            reviewAchada.comment = reviewNova.comment;
            reviewAchada.created_at = reviewNova.created_at;

            await _context.SaveChangesAsync();
            return reviewNova;
        }
        catch (Exception)
        {
            throw;
        }
    }
    
    // DELETE
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
}