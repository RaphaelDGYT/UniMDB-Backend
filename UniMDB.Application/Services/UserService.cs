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
                name = dto.name,
                username = dto.username,
                email = dto.email,
                password = dto.password
            };

            var createdUser = await _userRepository.CreateAsync(user);

            return new UserResponseAPI
            {
                id = createdUser.id_user,
                username = createdUser.username,
                email = createdUser.email
            };
        }

        public async Task<bool> DeleteUser(uint id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<UserResponseAPI?> GetUser(string email, string passworld)
        {
            var user = await _userRepository.GetByUserAsync(email, passworld);
            if (user == null) return null;
            return new UserResponseAPI { id = user.id_user, username = user.username, email = user.email };
        }

        public async Task<UserResponseAPI?> UpdateUser(uint id, UserRegistration dto)
        {
            var existing = await _userRepository.GetByIdAsync(id);
            if (existing == null) return null;

            existing.username = dto.username;
            existing.email = dto.email;
            if (!string.IsNullOrWhiteSpace(dto.password))
                existing.password = dto.password;

            var updated = await _userRepository.UpdateAsync(existing);
            return new UserResponseAPI { id = updated!.id_user, username = updated.username, email = updated.email };
        }

        
    
        
    }
}