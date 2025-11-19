using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;
using UniMDB.Domain.Interfaces;
using UniMDB.Infrastructure.Data;
using UniMDB.Infrastructure.Repositories;

namespace UniMDB.Application.Services;

public class FavoriteService : IFavoriteService
{
    private readonly IReviewRepository _reviewRepository;
    public FavoriteService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    // CREATE
    public async Task<ReviewResponse> AddFavorite(ReviewCreation review)
    {
        try
        {
            Review reviewAdicionar = new Review
            {
                id_review = 0,
                id_review_user = review.Id_User,
                id_movie_mdb = review.Id_Movie,
                review = review.Score,
                comment = review.Comment,
                created_at = DateTime.Now
            };

            Review reviewNova = await _reviewRepository.AddFavoriteAsync(reviewAdicionar);

            return new ReviewResponse
            {
                Id = reviewNova.id_review,
                User_Id = reviewAdicionar.id_review_user,
                Movie_Id = reviewAdicionar.id_movie_mdb,
                Score = reviewAdicionar.review,
                Comment = reviewAdicionar.comment,
                Created_at = reviewAdicionar.created_at
            };
        }
        catch
        {
            throw;
        }
    }
    
    public async Task<List<ReviewResponse>> AddBatchFavorite(List<ReviewCreation> reviews)
    {
        try
        {
            List<Review> batchReviews = new List<Review>();

            foreach (var review in reviews)
            {
                batchReviews.Add(new Review
                {
                    id_review = 0,
                    id_review_user = review.Id_User,
                    id_movie_mdb = review.Id_Movie,
                    review = review.Score,
                    comment = review.Comment,
                    created_at = DateTime.Now
                });
            }

            List<Review> reviewsAdicionadass = await _reviewRepository.AddBatchFavoriteAsync(batchReviews);

            // Resposta

            List<ReviewResponse> reviewsResponses = new List<ReviewResponse>();

            foreach (var review in reviewsAdicionadass)
            {
                reviewsResponses.Add(new ReviewResponse
                { 
                    Id = review.id_review,
                    User_Id = review.id_review_user,
                    Movie_Id = review.id_movie_mdb,
                    Score = review.review,
                    Comment = review.comment,
                    Created_at = review.created_at
                });
            }

            return reviewsResponses;
        }
        catch 
        {
            throw;
        }
    }
    
    // READ
    public async Task<ReviewResponse> GetFavoriteById(uint id_review)
    {
        Review? review = await _reviewRepository.GetReviewByIdAsync(id_review);

        if (review == null)
        {
            return new ReviewResponse();
        }

        return new ReviewResponse
        {
            Id = review.id_review,
            User_Id = review.id_review_user,
            Movie_Id = review.id_movie_mdb,
            Score = review.review,
            Comment = review.comment,
            Created_at = review.created_at
        };
    }   

    // public async Task<FavoriteResponse> GetAllFavoritesByUser<>
    // {
        
    // }
    
    // UPDATE
    public async Task<ReviewResponse> UpdateFavorite(uint id_review, ReviewUpdate review)
    {

        Review reviewAlterada = new Review
        {
            review = review.Score,
            comment = review.Comment
        };

        Review reviewNova = await _reviewRepository.UpdateReviewAsync(id_review, reviewAlterada);

        return new ReviewResponse
        {
            Id = id_review,
            User_Id = reviewNova.id_review_user,
            Movie_Id = reviewNova.id_movie_mdb,
            Score = reviewNova.review,
            Comment = reviewNova.comment,
            Created_at = reviewNova.created_at
        };
    }
    
    // DELETE
    public async Task<bool> DeleteFavorite(uint id_review)
    {
        return await _reviewRepository.DeleteReviewAsync(id_review);
    }

}