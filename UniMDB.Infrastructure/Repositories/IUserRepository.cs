using UniMDB.Domain.Entities;

namespace UniMDB.Domain.Interfaces;

/*

    Explicacao:
        
        Classe responsavel por criar os contratos para serem cumpridos na 'UserRepository', forçando 
        a implementacao de cada funcao aqui presente
 
*/

public interface IUserRepository
{
    // CREATE
    Task<User> AddUserAsync(User user);
    Task<List<User>> AddBatchUserAsync(List<User> users);

    // READ
    Task<List<uint>> GetAllUserIdsAsync();
    Task<User?> GetUserByIdAsync(uint id);
    Task<User?> GetUserBySessionAsync(User user);
    Task<List<Review>> GetAllReviewsByUserIdAsync(uint id);

    Task<List<Favorite>> GetAllFavoriteByUserIdAsync(uint id_user);
    //Task<User> GetUserByReview(uint reviewId);


    // UPDATE
    Task<User?> UpdateUserAsync(uint id, User userNovo);
    Task<List<User>> UpdateBatchUserAsync(List<(uint, User)> usersNovos);

    // DELETE
    Task<bool> DeleteUserAsync(uint id);
}