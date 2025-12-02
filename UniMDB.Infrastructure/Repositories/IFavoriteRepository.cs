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
    Task<Favorite> AddFavoriteAsync(Favorite favorite);
    Task<List<Favorite>> AddBatchFavoriteAsync(List<Favorite> favorites);

    // READ
    Task<Favorite?> GetFavoriteByIdAsync(uint id);
   


    
    // DELETE
    Task<bool> DeleteFavoriteAsync(uint id);

}