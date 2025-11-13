using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;

namespace UniMDB.Application.Services;

public interface IUserService
{
    // CREATE
    Task<UserResponseAPI> AddUser(UserRegistration user);
    Task<List<UserResponseAPI>> AddBatchUser(List<UserRegistration> users);

    // READ
    Task<List<uint>> GetAllUserIds();
    Task<UserResponseAPI> GetUserById(uint id);
    Task<UserResponseAPI> GetUserBySession(UserLogin user);
    Task<List<ReviewResponseAPI>> GetAllReviewsByUserId(uint id);

    //Task<User> GetUserByReview(uint reviewId);


    // UPDATE

    // Talvez isso possa gerar problemas no futuro, mas por enquanto ta de boa
    Task<User> UpdateUser(User user);
    Task<List<User>> UpdateBatchUser(List<User> users);

    // DELETE
    Task<bool> DeleteUser(uint id);
}