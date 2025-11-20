using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;
using UniMDB.Domain.Interfaces;
using UniMDB.Infrastructure.Data;
using UniMDB.Infrastructure.Repositories;

namespace UniMDB.Application.Services;

public interface IFavoriteService
{
    // CREATE
    Task<FavoriteResponse> AddFavorite(FavoriteCreation favorite);
    Task<List<FavoriteResponse>> AddBatchFavorite(List<FavoriteCreation> favorites);

    // READ
    Task<FavoriteResponse> GetFavoriteById(uint id_favorite);
    
    // DELETE
    Task<bool> DeleteFavorite(uint id_favorite);
}