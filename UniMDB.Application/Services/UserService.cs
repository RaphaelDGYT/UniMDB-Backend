using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;
using UniMDB.Domain.Interfaces;

namespace UniMDB.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserResponseAPI> AddUser(UserRegistration dto)
        {
            var user = new User
            {
                Name = dto.Name,
                Username = dto.Username,
                Email = dto.Email,
                Password = HashPassword(dto.Password)
            };

            var createdUser = await _userRepository.CreateAsync(user);

            return new UserResponseAPI
            {
                Id = createdUser.Id_user,
                Username = createdUser.Username,
                Email = createdUser.Email
            };
        }

        public async Task<bool> DeleteUser(uint id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<List<UserResponseAPI>> GetAllUsers()
        {
            var users = await _userRepository.GetAllAsync();
            var list = new List<UserResponseAPI>();
            foreach (var u in users)
            {
                list.Add(new UserResponseAPI { Id = u.Id_user, Username = u.Username, Email = u.Email });
            }
            return list;
        }

        public async Task<UserResponseAPI?> GetUser(uint id)
        {
            var u = await _userRepository.GetByIdAsync(id);
            if (u == null) return null;
            return new UserResponseAPI { Id = u.Id_user, Username = u.Username, Email = u.Email };
        }

        public async Task<UserResponseAPI?> UpdateUser(uint id, UserRegistration dto)
        {
            var existing = await _userRepository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.Username = dto.Username;
            existing.Email = dto.Email;
            if (!string.IsNullOrWhiteSpace(dto.Password))
                existing.Password = HashPassword(dto.Password);

            var updated = await _userRepository.UpdateAsync(existing);
            return new UserResponseAPI { Id = updated!.Id_user, Username = updated.Username, Email = updated.Email };
        }

        public async Task<User> GetUserByReview(uint id_review)
        {
            var user = await _userRepository.GetUserByReviewAsync(id_review);
            return user!;
        }

        // método simples de hash (SHA256). Em produção, use BCrypt/Argon2.
        
        private string HashPassword(string password)
        {
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(password);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
        
    }
}
