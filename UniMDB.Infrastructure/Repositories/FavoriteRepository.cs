using UniMDB.Infrastructure.Data;
using UniMDB.Domain.Interfaces;
using UniMDB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace UniMDB.Infrastructure.Repositories;

/*

    Explicacao:
        
        Classe responsavel por criar as funcoes que mexem de fato com o Banco de Dados, nesse caso envolvendo
        a classe 'User'
 
*/

public class FavoriteRepository : IFavoriteRepository
{
    private readonly ApplicationDbContext _context;
    public FavoriteRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<Favorite> AddFavoriteAsync(Favorite favorite)
    {
        try
        {
            await _context.Favorites.AddAsync(favorite);
            await _context.SaveChangesAsync();

            favorite.user = await _context.Users.FindAsync(favorite.id_user);

            return favorite;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<List<Favorite>> AddBatchFavoriteAsync(List<Favorite> favorites)
    {
        try
        {
            await _context.Favorites.AddRangeAsync(favorites);
            await _context.SaveChangesAsync();

            foreach (var favorite in favorites)
            {
                favorite.user = await _context.Users.FindAsync(favorite.id_user);
            }

            return favorites;
        }
        catch (Exception)
        {
            throw;
        }
    }
    
    // READ
    public async Task<Favorite?> GetFavoriteByIdAsync(uint id)
    {
        Favorite? favorite = await _context.Favorites.FindAsync(id);

        if (favorite == null)
        {
            return null;
        }

        favorite.user = await _context.Users.FindAsync(favorite.id_user);

        return favorite;
    }

    
   
    
    
    // DELETE
    public async Task<bool> DeleteFavoriteAsync(uint id)
    {
        try
        {
            int colunas_deletadas = await _context.Favorites
                                            .Where(r => r.id_favorite == id)
                                            .ExecuteDeleteAsync();

            if (colunas_deletadas < 1)
            {
                return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            throw;
        }
    }
}