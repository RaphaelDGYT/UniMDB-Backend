using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;

namespace UniMDB.Application.Services;

//  Um 'contrato' que obriga o UserServices a implementar todas essas funções
//  Na prática isso aqui é a nossa lista de funções presentes na API

public interface IUserService
{
    // CRUD
    Task<UserResponseAPI> GetUser(uint id);
    Task<UserResponseAPI> GetUserSession(UserLogin userSession);
    Task<UserResponseAPI> AddUser(UserRegistration user);
    Task<UserResponseAPI> UpdateUser(UserRegistration user);
    Task<bool> DeleteUser(uint id);

    // Outras
    //Task<User> GetUserByReview(uint id_review);
}