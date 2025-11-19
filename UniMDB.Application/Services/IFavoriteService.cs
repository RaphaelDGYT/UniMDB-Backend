using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;

namespace UniMDB.Application.Services;

public interface IFavoriteService
{
    // CREATE
    Task<ReviewResponse> AddFavorite(ReviewCreation review);
    Task<List<ReviewResponse>> AddBatchFavorite(List<ReviewCreation> reviews);

    // READ
    Task<ReviewResponse> GetFavoriteById(uint id_review);
    // Task<List<FavoriteResponse>> GetALLFavoritesByUser<List<Favorite>;

    // UPDATE
    Task<ReviewResponse> UpdateFavorite(uint id_review, ReviewUpdate review);
    
    // DELETE
    Task<bool> DeleteFavorite(uint id_review);
}