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

    // CREATE
    public async Task<UserResponseAPI> AddUser(UserRegistration user)
    {
        User userAdicionar = new User
        {
            id_user = 0,
            name = user.Name,
            username = user.Username,
            email = user.Email,
            password = user.Password
        };

        User userNovo = await _userRepository.AddUserAsync(userAdicionar);

        return new UserResponseAPI 
        {
            Id = userNovo.id_user,
            Name = user.Name,
            Username = user.Username,
            Email = user.Email,
            Password = user.Password
        };
    }
    public async Task<List<UserResponseAPI>> AddBatchUser(List<UserRegistration> users)
    {
        List<User> batchUsers = new List<User>();

        foreach (var user in users)
        {
            batchUsers.Add(new User 
            {
                id_user = 0,
                name = user.Name,
                username = user.Username,
                email = user.Email,
                password = user.Password
            });
        }

        List<User> usersAdicionados = await _userRepository.AddBatchUserAsync(batchUsers);

        List<UserResponseAPI> userResponseAPIs = new List<UserResponseAPI>();

        foreach (var user in usersAdicionados)
        {
            UserResponseAPI userAdicionado = new UserResponseAPI 
            {
                Id = user.id_user,
                Name = user.name,
                Username = user.username,
                Email = user.email,
                Password = user.password
            };

            userResponseAPIs.Add(userAdicionado);
        }

        return userResponseAPIs;
    }

    // READ
    public async Task<List<uint>> GetAllUserIds()
    {
        return await _userRepository.GetAllUserIdsAsync();
    }
    public async Task<UserResponseAPI> GetUserById(uint id)
    {
        var user = await _userRepository.GetUserByIdAsync(id);
    
        if (user == null)
        {
            return null;
        }

        return new UserResponseAPI 
        {
            Id = id,
            Name = user.name,
            Username = user.username,
            Email = user.email,
            Password = user.password
        };
    }
    public async Task<UserResponseAPI> GetUserBySession(UserLogin user)
    {
        User userProcurar = new User 
        {
            username = user.Username,
            password = user.Password,
            email = user.Email
        };

        var userAchado = await _userRepository.GetUserBySessionAsync(userProcurar);

        if (userAchado == null)
        {
            return null;
        }

        return new UserResponseAPI
        {
            Id = userAchado.id_user,
            Name = userAchado.name,
            Username = userAchado.username,
            Email = userAchado.email,
            Password = userAchado.password
        };
    }
    public async Task<List<ReviewResponseAPI>> GetAllReviewsByUserId(uint id)
    {

        List<Review> reviews = await _userRepository.GetAllReviewsByUserIdAsync(id);

        if (reviews.Count == 0)
        {
            return Enumerable.Empty<ReviewResponseAPI>().ToList();
        }

        User user = await _userRepository.GetUserByIdAsync(id);

        List<ReviewResponseAPI> reviewsResponseAPIs = new List<ReviewResponseAPI>();

        foreach (var review in reviews)
        {

            ReviewResponseAPI reviewResponse = new ReviewResponseAPI
            {
                Id = review.id_review,
                Score = review.review,
                Comment = review.comment,
                Created_at = review.created_at,
                User = user
            };

            reviewsResponseAPIs.Add(reviewResponse);
        }

        return reviewsResponseAPIs;
    }

    // UPDATE
    public async Task<List<User>> UpdateBatchUser(List<User> users)
    {
        return await _userRepository.UpdateBatchUserAsync(users);
    }
    public async Task<User> UpdateUser(User user)
    {
        return await _userRepository.UpdateUserAsync(user);
    }
    
    // DELETE
    public async Task<bool> DeleteUser(uint id)
    {
        return await _userRepository.DeleteUserAsync(id);
    }

}