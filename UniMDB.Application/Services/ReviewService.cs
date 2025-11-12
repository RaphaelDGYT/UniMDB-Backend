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


    public async Task<ReviewResponseAPI> AddReview(ReviewCreation review)
    {
        try
        {

            var reviewNova = await _reviewRepository.AddReviewAsync(new Review
            {

                id_review = 0,
                id_review_user = review.Id_User,
                id_movie_mdb = review.Id_Movie,
                review = review.Score,
                comment = review.Comment,
                created_at = DateTime.Now

            });

            var userReview = await _reviewRepository.GetUserByReviewAsync(reviewNova.id_review);

            return new ReviewResponseAPI
            {
                Id = reviewNova.id_review,
                Score = review.Score,
                Comment = review.Comment,
                Created_at = reviewNova.created_at,
                User = userReview
            };

        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<bool> DeleteReview(uint id_review)
    {
        return await _reviewRepository.DeleteReviewAsync(id_review);
    }

    public async Task<List<ReviewResponseAPI>> GetAllReviewsByUser(uint id_user)
    {
        try
        {
            List<ReviewResponseAPI> lista_reviews = new List<ReviewResponseAPI>();

            var reviews = await _reviewRepository.GetAllReviewsByUser(id_user);

            foreach (var review in reviews)
            {
                ReviewResponseAPI reviewResponse = new ReviewResponseAPI()
                { 
                    Id = review.id_review,
                    Score = review.review,
                    Comment = review.comment,
                    Created_at = review.created_at
                };

                lista_reviews.Add(reviewResponse);
            }

            return lista_reviews;
        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task<ReviewResponseAPI> GetReviewById(uint id_review)
    {
        var review = await _reviewRepository.GetReviewByIdAsync(id_review);
        var user = await _reviewRepository.GetUserByReviewAsync(id_review);

        return new ReviewResponseAPI
        {
            Id = review.id_review,
            Score = review.review,
            Comment = review.comment,
            Created_at = review.created_at,
            User = user
        };
    }

    public async Task<ReviewResponseAPI> UpdateReview(uint id_review, ReviewCreation review)
    {
        var reviewExistente = await _reviewRepository.GetReviewByIdAsync(id_review);

        if (reviewExistente == null)
        {
            return null;
        }

        var user = await _reviewRepository.GetUserByReviewAsync(id_review);

        Review novaReview = new Review
        {
            id_review = id_review,
            id_review_user = reviewExistente.id_review_user,
            id_movie_mdb = reviewExistente.id_movie_mdb,
            review = review.Score,
            comment = review.Comment,
            created_at = reviewExistente.created_at
        };

        await _reviewRepository.UpdateReviewAsync(novaReview);

        return new ReviewResponseAPI
        {
            Id = id_review,
            Score = novaReview.review,
            Comment = novaReview.comment,
            Created_at = novaReview.created_at,
            User = user
        };
    }
}