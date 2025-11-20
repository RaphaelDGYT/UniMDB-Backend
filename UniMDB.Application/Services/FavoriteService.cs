using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;
using UniMDB.Domain.Interfaces;
using UniMDB.Infrastructure.Data;
using UniMDB.Infrastructure.Repositories;

namespace UniMDB.Application.Services;

public class FavoriteService : IFavoriteService
{
    private readonly IFavoriteRepository _favoriteRepository;
    public FavoriteService(IFavoriteRepository favoriteRepository)
    {
        _favoriteRepository = favoriteRepository;
    }

    // CREATE
    public async Task<FavoriteResponse> AddFavorite(FavoriteCreation favorite)
    {
        try
        {
            Favorite favoriteAdicionar = new Favorite
            {
                id_favorite = 0,
                id_user = favorite.Id_User,
                id_movie_mdb = favorite.Id_Movie_Mdb,
            };

            Favorite FavoriteNovo = await _favoriteRepository.AddFavoriteAsync(favoriteAdicionar);

            return new FavoriteResponse
            {
                Id_Favorite = FavoriteNovo.id_favorite,
                Id_User = FavoriteNovo.id_user,
                Id_Movie_Mdb = FavoriteNovo.id_movie_mdb,
                
            };
        }
        catch
        {
            throw;
        }
    }
    
    public async Task<List<FavoriteResponse>> AddBatchFavorite(List<FavoriteCreation> favorites)
    {
        try
        {
            List<Favorite> batchFavorites = new List<Favorite>();

            foreach (var favorite in favorites)
            {
                batchFavorites.Add(new Favorite
                {
                    id_favorite = 0,
                    id_user = favorite.Id_User,
                    id_movie_mdb = favorite.Id_Movie_Mdb,
                    
                });
            }

            List<Favorite> favoritesAdicionadas = await _favoriteRepository.AddBatchFavoriteAsync(batchFavorites);

            // Resposta

            List<FavoriteResponse> favoriteResponses = new List<FavoriteResponse>();

            foreach (var favorite in favoritesAdicionadas)
            {
                favoriteResponses.Add(new FavoriteResponse
                { 
                    Id_Favorite= favorite.id_favorite,
                    Id_User = favorite.id_user,
                    Id_Movie_Mdb = favorite.id_movie_mdb,
                   
                });
            }
            return favoriteResponses;
        }
        catch 
        {
            throw;
        }
    }
    
    // READ
    public async Task<FavoriteResponse> GetFavoriteById(uint id_favorite)
    {
        Favorite? favorite = await _favoriteRepository.GetFavoriteByIdAsync(id_favorite);

        if (favorite == null)
        {
            return new FavoriteResponse();
        }

        return new FavoriteResponse
        {
            Id_Favorite = favorite.id_favorite,
            Id_User = favorite.id_user,
            Id_Movie_Mdb = favorite.id_movie_mdb,
            
        };
    }   

   
    
    
    // DELETE
    public async Task<bool> DeleteFavorite(uint id_favorite)
    {
        return await _favoriteRepository.DeleteFavoriteAsync(id_favorite);
    }

}