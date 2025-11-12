using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;
using UniMDB.Domain.Interfaces;

namespace UniMDB.Application.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<UserResponseAPI> AddUser(UserRegistration user)
    {
        try
        {

            var userNovo = await _userRepository.AddUserAsync(new User {

                id_user = 0,
                name = user.Name,
                username = user.Username,
                email = user.Email,
                password = user.Password

            });

            return new UserResponseAPI
            {
                Id = userNovo.id_user,
                Name = userNovo.name,
                Username = userNovo.username,
                Email = userNovo.email,
                Password = userNovo.password,
                Ids_reviews = new List<uint>()
            };

        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<bool> DeleteUser(uint id)
    {
        return await _userRepository.DeleteUserAsync(id);
    }
    public async Task<UserResponseAPI> GetUser(uint id)
    {
        try
        {

            var user = await _userRepository.GetUserByIdAsync(id);
            var review = await _userRepository.GetAllReviewsByUserIdAsync(id);

            List<uint> ids_reviews = review.Select(r => r.id_review).ToList();

            return new UserResponseAPI {
                Id = user.id_user,
                Name = user.name,
                Username = user.username,
                Email = user.email,
                Password = user.password,
                Ids_reviews = ids_reviews
            };

        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<UserResponseAPI> GetUserSession(UserLogin userSession)
    {
        try
        {

            var user = await _userRepository.GetUserBySessionAsync(new User 
            { 
                username = userSession.Username,
                password = userSession.Password,
                email = userSession.Email
            });

            if (user == null)
            {
                return null;
            }

            var review = await _userRepository.GetAllReviewsByUserIdAsync(user.id_user);

            List<uint> ids_reviews = review.Select(r => r.id_review).ToList();

            return new UserResponseAPI
            {
                Id = user.id_user,
                Name = user.name,
                Username = user.username,
                Email = user.email,
                Password = user.password,
                Ids_reviews = ids_reviews
            };

        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<UserResponseAPI> UpdateUser(UserRegistration user)
    {
        try
        {
            var userExistente = await _userRepository.GetUserBySessionAsync(new User
            {
                username = user.Username,
                password = user.Password,
                email = user.Email
            });

            var userNovo = new User()
            {
                id_user = userExistente.id_user,
                email = user.Email,
                name = user.Name,
                password = user.Password,
                username = user.Username
            };

            await _userRepository.UpdateUserAsync(userNovo);

            var review = await _userRepository.GetAllReviewsByUserIdAsync(userNovo.id_user);

            List<uint> ids_reviews = review.Select(r => r.id_review).ToList();

            return new UserResponseAPI
            {
                Id = userNovo.id_user,
                Name = userNovo.name,
                Username = userNovo.username,
                Email = userNovo.email,
                Password = userNovo.password,
                Ids_reviews = ids_reviews
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    /*
        Task<User> IUserService.GetUserByReview(uint id_review)
        {
            throw new NotImplementedException();
        }
    */

}