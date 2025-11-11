using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;

namespace UniMDB.Application.Services;

//  Um 'contrato' que obriga o UserServices a implementar todas essas funções
//  Na prática isso aqui é a nossa lista de funções presentes na API

public interface IUserService
{
    // CRUD
    Task<UserResponseAPI> GetUser(string email, string passworld);
    Task<UserResponseAPI> AddUser(UserRegistration user);
    Task<UserResponseAPI> UpdateUser(uint id, UserRegistration user);
    Task<bool> DeleteUser(uint id);

}