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
    Task<UserResponseAPI> UpdateUser(uint id, UserRegistration userNovo);
    Task<List<UserResponseAPI>> UpdateBatchUser(List<(uint, UserRegistration)> usersNovos);

    // DELETE
    Task<bool> DeleteUser(uint id);
}