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
            await _context.Reviews.AddAsync(review);
            await _context.SaveChangesAsync();

            review.user = await _context.Users.FindAsync(review.id_review_user);

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
            await _context.Reviews.AddRangeAsync(reviews);
            await _context.SaveChangesAsync();

            foreach (var review in reviews)
            {
                review.user = await _context.Users.FindAsync(review.id_review_user);
            }

            return reviews;
        }
        catch (Exception)
        {
            throw;
        }
    }
    
    // READ
    public async Task<Review?> GetReviewByIdAsync(uint id)
    {
        Review? review = await _context.Reviews.FindAsync(id);

        if (review == null)
        {
            return null;
        }

        review.user = await _context.Users.FindAsync(review.id_review_user);

        return review;
    }

    /*
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
    */

    // UPDATE
    public async Task<Review?> UpdateReviewAsync(uint id, Review reviewNova)
    {
        try
        {
            Review? reviewAchada = await GetReviewByIdAsync(id);

            if (reviewAchada == null)
            {
                return null;
            }

            int colunas_alteradas = await _context.Reviews
                                            .Where(r => r.id_review == id)
                                            .ExecuteUpdateAsync(update =>

                                                update
                                                    .SetProperty(r => r.review, reviewNova.review)
                                                    .SetProperty(r => r.comment, reviewNova.comment)

                                            );

            reviewNova.id_review = reviewAchada.id_review;
            reviewNova.id_review_user = reviewAchada.id_review_user;
            reviewNova.user = reviewAchada.user;
            reviewNova.id_movie_mdb = reviewAchada.id_movie_mdb;
            reviewNova.created_at = reviewAchada.created_at;

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
            int colunas_deletadas = await _context.Reviews
                                            .Where(r => r.id_review == id)
                                            .ExecuteDeleteAsync();

            if (colunas_deletadas < 1)
            {
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }
}