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
            /*
                
                Lógica:

                    Espera até o Banco de Dados adicionar o parâmetro 'user', se der algum erro será enviada 
                    uma Exception
             
            */

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
            /*

                Lógica:

                    Espera até o Banco de Dados adicionar todos os usuários  dentro do parâmetro 'users', 
                    se der algum erro será enviada uma Exception

            */

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
        /*

            Lógica:

                Primeiro ele "lê" a tabela 'Users' como uma Query não Trackeavel, ou seja, uma Query que não fará
                mudanças dentro da tabela, somente leitura.

                Segundo ele seleciona e retorna todos os IDs dos usuários

                Terceiro ele converte para uma lista assincrona, ou seja, o programa esperará até que tudo
                tenha sido feito para enviar a Lista dos IDs

            
                Caso retorne nulo, ele retornará uma lista vázia de IDs

        */

        return await _context.Users
                        .AsNoTracking()
                        .Select(u => u.id_user)
                        .ToListAsync()
                        ??
                        Enumerable.Empty<uint>().ToList();
    }
    public async Task<User?> GetUserByIdAsync(uint id)
    {
        /*

            Lógica:

                Espera até o Banco de Dados achar um usuário com o mesmo id do parâmetro, caso ele não ache
                retornará um valor nulo

        */

        return await _context.Users.FindAsync(id);
    }
    public Task<User?> GetUserBySessionAsync(User userSession)
    {
        try
        {
            /*

                Lógica:

                    Primeiro ele "lê" a tabela 'Users' como uma Query não Trackeavel, ou seja, 
                    uma Query que não fará mudanças dentro da tabela, somente leitura.

                    Segundo ele retorna o primeiro usuário que ele achar com os mesmos
                    username, senha e e-mail do parâmetro 'userSession'. 

                    Caso não ache, ele retorna um valor nulo.

            */

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
            /*

                Lógica:

                    Primeiro ele "lê" a tabela 'Reviews' como uma Query não Trackeavel, ou seja, 
                    uma Query que não fará mudanças dentro da tabela, somente leitura.

                    Segundo ele retorna todas as reviews com o mesmo id passado no parâmetro

                    Terceiro ele converte para uma lista assincrona, ou seja, o programa esperará até que tudo
                    tenha sido feito para enviar a Lista de Reviews


                    Caso retorne nulo, ele retornará uma lista vázia de IDs

            */

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
            /*

                Lógica:

                    Primeiro ele verifica se o usuário com esse ID passado no parâmetro existe.
                    Caso não, ele retornará nulo

                    Segundo ele procura por todos os usuários com esse mesmo ID 

                    Terceiro ele executa um Update que muda o nome, username, e-mail e senha para 
                    os passados no parâmetro 'userNovo'

                    Por fim, retorna o mesmo 'userNovo' passado, só que alterado o ID para o usuário
                    que foi alterado só para ter certeza que ele está com ID certo

            */

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