using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;

namespace UniMDB.Application.Services;

public interface IUserService
{
    // CREATE
    Task<UserResponse> AddUser(UserCreation user);
    Task<List<UserResponse>> AddBatchUser(List<UserCreation> users);
    Task<UserResponse> Login(UserLogin user);

    // READ
    Task<List<uint>> GetAllUserIds();
    Task<UserResponse> GetUserById(uint id);
    // Task<UserResponse> GetUserBySession(UserLogin user);
    Task<UserReviewsResponse> GetAllReviewsByUserId(uint id);


    //Task<User> GetUserByReview(uint reviewId);


    // UPDATE
    Task<UserResponse> UpdateUser(uint id, UserCreation userNovo);
    Task<List<UserResponse>> UpdateBatchUser(List<(uint, UserCreation)> usersNovos);

    // DELETE
    Task<bool> DeleteUser(uint id);
}