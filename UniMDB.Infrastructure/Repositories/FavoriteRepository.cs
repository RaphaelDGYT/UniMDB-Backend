// using UniMDB.Infrastructure.Data;
// using UniMDB.Domain.Interfaces;
// using UniMDB.Domain.Entities;
// using Microsoft.EntityFrameworkCore;

// namespace UniMDB.Infrastructure.Repositories;

// /*

//     Explicacao:
        
//         Classe responsavel por criar as funcoes que mexem de fato com o Banco de Dados, nesse caso envolvendo
//         a classe 'User'
 
// */

// public class FavoriteRepository : IFavoriteRepository
// {
//     private readonly ApplicationDbContext _context;
//     public FavoriteRepository(ApplicationDbContext context)
//     {
//         _context = context;
//     }
//     public async Task<Favorite> AddFavoriteAsync(Favorite favorite)
//     {
//         try
//         {
//             await _context.Favorites.AddAsync();
//             await _context.SaveChangesAsync();

//             favorite.user = await _context.Users.FindAsync(favorite.id_user);

//             return favorite;
//         }
//         catch (Exception)
//         {
//             throw;
//         }
//     }
//     public async Task<List<Review>> AddBatchFavoriteAsync(List<Review> reviews)
//     {
//         try
//         {
//             await _context.Favorites.AddRangeAsync(Favorite);
//             await _context.SaveChangesAsync();

//             foreach (var favorite in favorites)
//             {
//                 favorite.user = await _context.Users.FindAsync(favorite.id_review_user);
//             }

//             return reviews;
//         }
//         catch (Exception)
//         {
//             throw;
//         }
//     }
    
//     // READ
//     public async Task<Review?> GetFavoriteByIdAsync(uint id)
//     {
//         Favorite? favorite = await _context.Favorites.FindAsync(id);

//         if (favorite == null)
//         {
//             return null;
//         }

//         favorite.user = await _context.Users.FindAsync(favorite.id_user);

//         return favorite;
//     }

    
//     public async Task<List<Review>> GetAllFavoriteByUserIdAsync(uint id_user)
//     {
//         try
//         {
//             return await _context.Reviews
//                                     .AsNoTracking()
//                                     .Where(f => f.id_favorite == id_user)
//                                     .ToListAsync()
//                                     ??
//                                     Enumerable.Empty<Review>().ToList();
//         }
//         catch (Exception)
//         {
//             throw;
//         }
//     }
    

// // UPDATE
// public async Task<Review?> UpdateReviewAsync(uint id, Review reviewNova)
//     {
//         try
//         {
//             Review? reviewAchada = await GetReviewByIdAsync(id);

//             if (reviewAchada == null)
//             {
//                 return null;
//             }

//             int colunas_alteradas = await _context.Reviews
//                                             .Where(r => r.id_review == id)
//                                             .ExecuteUpdateAsync(update =>

//                                                 update
//                                                     .SetProperty(r => r.review, reviewNova.review)
//                                                     .SetProperty(r => r.comment, reviewNova.comment)

//                                             );

//             reviewNova.id_review = reviewAchada.id_review;
//             reviewNova.id_review_user = reviewAchada.id_review_user;
//             reviewNova.user = reviewAchada.user;
//             reviewNova.id_movie_mdb = reviewAchada.id_movie_mdb;
//             reviewNova.created_at = reviewAchada.created_at;

//             await _context.SaveChangesAsync();
//             return reviewNova;
//         }
//         catch (Exception)
//         {
//             throw;
//         }
//     }
    
//     // DELETE
//     public async Task<bool> DeleteReviewAsync(uint id)
//     {
//         try
//         {
//             int colunas_deletadas = await _context.Reviews
//                                             .Where(r => r.id_review == id)
//                                             .ExecuteDeleteAsync();

//             if (colunas_deletadas < 1)
//             {
//                 return false;
//             }

//             await _context.SaveChangesAsync();
//             return true;
//         }
//         catch (Exception)
//         {
//             throw;
//         }
//     }
// }