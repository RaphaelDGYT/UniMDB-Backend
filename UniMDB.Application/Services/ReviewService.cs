using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;
using UniMDB.Domain.Interfaces;
using UniMDB.Infrastructure.Data;
using UniMDB.Infrastructure.Repositories;

namespace UniMDB.Application.Services;

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    // CREATE
    public async Task<ReviewResponse> AddReview(ReviewCreation review)
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

            Review reviewNova = await _reviewRepository.AddReviewAsync(reviewAdicionar);

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
    
    public async Task<List<ReviewResponse>> AddBatchReview(List<ReviewCreation> reviews)
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

            List<Review> reviewsAdicionadass = await _reviewRepository.AddBatchReviewAsync(batchReviews);

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
    public async Task<ReviewResponse> GetReviewById(uint id_review)
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
    public async Task<ReviewsMoviesResponse> GetAllReviewsByMovieId(string id_movie)
    {
    List<Review> reviews = await _reviewRepository.GetAllReviewsByMovieIdAsync(id_movie);

    if (reviews == null || reviews.Count == 0)
    {
        return new ReviewsMoviesResponse
        {
            Movies_Id = id_movie,
            Reviews = new List<ReviewResponseList>()
        };
    }

    return new ReviewsMoviesResponse
    {
        Movies_Id = id_movie,
        Reviews = reviews.Select(r => new ReviewResponseList
        {
            Id = r.id_review,
            User_Id = r.id_review_user,
            Movie_Id = r.id_movie_mdb,
            Score = r.review,
            Comment = r.comment,
            Created_at = r.created_at
        }).ToList()
    };
    }
    // UPDATE
    public async Task<ReviewResponse> UpdateReview(uint id_review, ReviewUpdate review)
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
    public async Task<bool> DeleteReview(uint id_review)
    {
        return await _reviewRepository.DeleteReviewAsync(id_review);
    }

}