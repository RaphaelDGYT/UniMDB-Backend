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

    public Task<Review> AddReviewAsync(Review review)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteReviewAsync(uint id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Review>> GetAllReviewsByUser(uint userId)
    {
        throw new NotImplementedException();
    }

    public Task<Review> GetReviewByIdAsync(uint id)
    {
        throw new NotImplementedException();
    }

    public Task<Review> UpdateReviewAsync(Review review)
    {
        throw new NotImplementedException();
    }

}