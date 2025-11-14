using UniMDB.Infrastructure.Data;
using UniMDB.Domain.Interfaces;
using UniMDB.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace UniMDB.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // CREATE
    public async Task<User> AddUserAsync(User user)
    {
        try
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch 
        {
            throw new Exception("Erro ao adicionar usuário");
        }
    }
    public async Task<List<User>> AddBatchUserAsync(List<User> users)
    {
        try
        {
            await _context.Users.AddRangeAsync(users);
            await _context.SaveChangesAsync();
            return users;
        }
        catch (Exception)
        {
            throw;
        }
    }

    // READ
    public async Task<List<uint>> GetAllUserIdsAsync()
    {
        return await _context.Users
                        .AsNoTracking()
                        .Select(u => u.id_user)
                        .ToListAsync()
                        ??
                        Enumerable.Empty<uint>().ToList();
    }
    public async Task<User?> GetUserByIdAsync(uint id)
    {
        return await _context.Users.FindAsync(id);
    }
    public Task<User?> GetUserBySessionAsync(User userSession)
    {

        try
        {
            return _context.Users
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u =>
                            u.username == userSession.username &&
                            u.password == userSession.password &&
                            u.email == userSession.email
                        );
        }
        catch (Exception)
        {
            throw;
        }

    }
    public async Task<List<Review>> GetAllReviewsByUserIdAsync(uint id)
    {
        try
        {
            return await _context.Reviews
                                    .AsNoTracking()
                                    .Where(r => r.id_review_user == id)
                                    .ToListAsync()
                                    ??
                                    Enumerable.Empty<Review>().ToList();

        }
        catch (Exception)
        {
            throw;
        }
    }
    
    // UPDATE
    public async Task<User?> UpdateUserAsync(uint id, User userNovo)
    {
        try
        {
            User? userAchado = await GetUserByIdAsync(id);

            if (userAchado == null)
            {
                return null;
            }

            int colunas_alteradas = await _context.Users
                                            .Where(u => u.id_user == id)
                                            .ExecuteUpdateAsync(update =>

                                                update
                                                    .SetProperty(u => u.name, userNovo.name)
                                                    .SetProperty(u => u.username, userNovo.username)
                                                    .SetProperty(u => u.email, userNovo.email)
                                                    .SetProperty(u => u.password, userNovo.password)

                                            );

            userNovo.id_user = userAchado.id_user;

            await _context.SaveChangesAsync();
            return userNovo;
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<List<User>> UpdateBatchUserAsync(List<(uint, User)> users)
    {
        try
        {
            if (users.Count == 0)
            {
                return Enumerable.Empty<User>().ToList();
            }

            List<User> usersAtualizados = new List<User>(users.Count);

            foreach (var item in users)
            {
                User userNovo = item.Item2;
                uint userNovoID = item.Item1;

                User? userAchado = await GetUserByIdAsync(userNovoID);

                if (userAchado == null)
                {
                    continue;
                }

                int colunas_alteradas = await _context.Users
                                                .Where(u => u.id_user == userNovoID)
                                                .ExecuteUpdateAsync(update =>

                                                    update
                                                        .SetProperty(u => u.name, userNovo.name)
                                                        .SetProperty(u => u.username, userNovo.username)
                                                        .SetProperty(u => u.email, userNovo.email)
                                                        .SetProperty(u => u.password, userNovo.password)

                                                );

                userNovo.id_user = userAchado.id_user;

                usersAtualizados.Add(item.Item2);
            }

            await _context.SaveChangesAsync();
            return usersAtualizados;
        }
        catch (Exception)
        {
            throw;
        }

    }
    // DELETE
    public async Task<bool> DeleteUserAsync(uint id)
    {
        try
        {
            int colunas_deletadas = await _context.Users
                                            .Where(u => u.id_user == id)
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