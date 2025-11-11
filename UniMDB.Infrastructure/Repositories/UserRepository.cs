using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniMDB.Domain.Entities;
using UniMDB.Domain.Interfaces;
using UniMDB.Infrastructure.Data;

namespace UniMDB.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        // implementação do usuario ao banco de dados.
        public async Task<User> CreateAsync(User user)
        {
            _context.Users.Add(user);
            try
            {
                await _context.SaveChangesAsync();
                return user;
            }
            catch (DbUpdateException ex)
            {
                // mostra possiveis erros no banco de dados
                Console.WriteLine("Erro ao salvar no banco: " + ex.InnerException?.Message ?? ex.Message);
                throw; 
            }
        }

        public async Task<User?> GetByUserAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.email == email && u.password == password);
        }
        public async Task<User?> GetByIdAsync(uint id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User?> UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
        public async Task<bool> DeleteAsync(uint id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}