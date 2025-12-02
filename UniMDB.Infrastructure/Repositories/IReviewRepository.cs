using UniMDB.Domain.Entities;

namespace UniMDB.Domain.Interfaces;

/*

    Explicacao:
        
        Classe responsavel por criar os contratos para serem cumpridos na 'RevieRepository', forçando 
        a implementacao de cada funcao aqui presente
 
*/
public interface IReviewRepository
{
    // CREATE
    Task<Review> AddReviewAsync(Review review);
    Task<List<Review>> AddBatchReviewAsync(List<Review> reviews);

    // READ
    Task<Review?> GetReviewByIdAsync(uint id);
    Task<List<Review>> GetAllReviewsByMovieIdAsync(string id_movie);

    // UPDATE
    Task<Review?> UpdateReviewAsync(uint id, Review reviewNova);
    
    // DELETE
    Task<bool> DeleteReviewAsync(uint id);

}