using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;
using UniMDB.Domain.Interfaces;
using UniMDB.Infrastructure.Data;
using UniMDB.Infrastructure.Repositories;

namespace UniMDB.Application.Services;

//  Aqui vai ficar todas as implementações que envolvam o banco de dados e os usuários

//  TODO: Fazer as implementações   xD

public class ReviewService : IReviewService
{
    private readonly IReviewRepository _reviewRepository;
    public ReviewService(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }

    // CREATE
    public async Task<ReviewResponseAPI> AddReview(ReviewCreation review)
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

        return new ReviewResponseAPI
        {
            Id = reviewNova.id_review,
            Score = reviewAdicionar.review,
            Comment = reviewAdicionar.comment,
            Created_at = reviewAdicionar.created_at,
            User = reviewNova.user
        };
    }
    public async Task<List<ReviewResponseAPI>> AddBatchReview(List<ReviewCreation> reviews)
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

        List<ReviewResponseAPI> reviewResponseAPIs = new List<ReviewResponseAPI>();

        foreach (var review in reviewsAdicionadass)
        {
            ReviewResponseAPI reviewAdicionada = new ReviewResponseAPI
            {
                Id = review.id_review,
                Score = review.review,
                Comment = review.comment,
                Created_at = review.created_at,
                User = review.user
            };

            reviewResponseAPIs.Add(reviewAdicionada);
        }

        return reviewResponseAPIs;
    }
    
    // READ
    public async Task<ReviewResponseAPI> GetReviewById(uint id_review)
    {
        var review = await _reviewRepository.GetReviewByIdAsync(id_review);

        if (review == null)
        {
            return null;
        }

        return new ReviewResponseAPI
        {
            Id = review.id_review,
            Score = review.review,
            Comment = review.comment,
            Created_at = review.created_at,
            User = review.user
        };
    }   

    
    // FIX: Arrumar o retorno de User da ReviewResponse
    public async Task<List<ReviewResponseAPI>> GetAllReviewsByUserId(uint id)
        {
            List<Review> reviews = await _reviewRepository.GetAllReviewsByUserIdAsync(id);

            var x = reviews.First().user;


            List<ReviewResponseAPI> reviewsResponsesAPI = new List<ReviewResponseAPI>(reviews.Count);

            foreach (var review in reviews)
            {
                ReviewResponseAPI reviewResponse = new ReviewResponseAPI
                {
                    Id = review.id_review,
                    Score = review.review,
                    Comment = review.comment,
                    Created_at = review.created_at,
                    User = x
                };

                reviewsResponsesAPI.Add(reviewResponse);
            }

            return reviewsResponsesAPI;
        }
    
    // UPDATE
    public async Task<ReviewResponseAPI> UpdateReview(uint id_review, ReviewCreation review)
    {
        Review reviewAlterada = new Review
        {
            id_review = id_review,
            id_review_user = review.Id_User,
            id_movie_mdb = review.Id_Movie,
            review = review.Score,
            comment = review.Comment
        };

        Review reviewNova = await _reviewRepository.UpdateReviewAsync(id_review, reviewAlterada);

        return new ReviewResponseAPI
        {
            Id = id_review,
            Score = reviewNova.review,
            Comment = reviewNova.comment,
            Created_at = reviewNova.created_at,
            User = reviewNova.user
        };
    }
    
    // DELETE
    public async Task<bool> DeleteReview(uint id_review)
    {
        return await _reviewRepository.DeleteReviewAsync(id_review);
    }

}