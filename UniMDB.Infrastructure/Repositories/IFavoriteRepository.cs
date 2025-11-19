using UniMDB.Domain.Entities;
namespace UniMDB.Domain.Interfaces;
/*

    Explicacao:
        
        Classe responsavel por criar os contratos para serem cumpridos na 'RevieRepository', forçando 
        a implementacao de cada funcao aqui presente
 
*/
public interface IFavoriteRepository
{
    // CREATE
    Task<Review> AddFavoriteAsync(Favorite favorite);
    Task<List<Review>> AddBatchFavoriteAsync(List<Favorite> favorites);

    // READ
    Task<Review?> GetFavoriteByIdAsync(uint id);
    Task<List<Review>> GetAllFavoriteByUserIdAsync(uint id_user);

    // UPDATE
    Task<Review?> UpdateFavoriteAsync(uint id, Favorite FavoriteNova);
    
    // DELETE
    Task<bool> DeleteFavoriteAsync(uint id);

}