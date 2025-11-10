using UniMDB.Application.Dtos;

namespace UniMDB.Application.Services;

//  Um 'contrato' que obriga o ReviewServices a implementar todas essas funções
//  Na prática isso aqui é a nossa lista de funções presentes na API

public interface IReviewService
{
    // CRUD
    Task<ReviewResponse> GetReviewById(uint id_review);
    Task<List<ReviewResponse>> GetAllReviewsByUser(uint id_user);
    Task<ReviewCreation> AddReview(ReviewCreation review);
    Task<ReviewResponse> UpdateReview(uint id_review, ReviewCreation review);
    Task<bool> DeleteReview(uint id_review);
}