using UniMDB.Application.Dtos;

namespace UniMDB.Application.Services;

//  Um 'contrato' que obriga o ReviewServices a implementar todas essas funções
//  Na prática isso aqui é a nossa lista de funções presentes na API

public interface IReviewService
{
    // CRUD
    Task<ReviewResponseAPI> GetReviewById(uint id_review);
    Task<List<ReviewResponseAPI>> GetAllReviewsByUser(uint id_user);
    Task<ReviewResponseAPI> AddReview(ReviewCreation review);
    Task<ReviewResponseAPI> UpdateReview(uint id_review, ReviewCreation review);
    Task<bool> DeleteReview(uint id_review);
}