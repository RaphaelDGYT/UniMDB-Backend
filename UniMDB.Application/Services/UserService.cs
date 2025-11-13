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
            Password = user.Password,
            Ids_reviews = Enumerable.Empty<uint>().ToList()
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
                Password = user.password,
                Ids_reviews = Enumerable.Empty<uint>().ToList()
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

        var reviews = await _userRepository.GetAllReviewsByUserIdAsync(id);


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
            Password = user.password,
            Ids_reviews = reviews.Select(u => u.id_review).ToList() ?? Enumerable.Empty<uint>().ToList()
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
    public async Task<UserResponseAPI> UpdateUser(uint id, UserRegistration user)
    {
        User userAlterado = new User
        {
            id_user = id,
            name = user.Name,
            username = user.Username,
            email = user.Email,
            password = user.Password
        };

        User userNovo = await _userRepository.UpdateUserAsync(id, userAlterado);

        var reviews = await _userRepository.GetAllReviewsByUserIdAsync(id);

        return new UserResponseAPI
        {
            Id = userNovo.id_user,
            Name = userNovo.name,
            Username = userNovo.username,
            Email = userNovo.email,
            Password = userNovo.password,
            Ids_reviews = reviews.Select(r => r.id_review).ToList() ?? Enumerable.Empty<uint>().ToList()
        };
    }
    public async Task<List<UserResponseAPI>> UpdateBatchUser(List<(uint, UserRegistration)> users)
    {
        List<(uint, User)> usersAlterados = new List<(uint, User)>();

        foreach (var user in users)
        {
            User userAlterado = new User
            {
                id_user = user.Item1,
                name = user.Item2.Name,
                username = user.Item2.Username,
                email = user.Item2.Email,
                password = user.Item2.Password
            };

            usersAlterados.Add((user.Item1, userAlterado));
        }

        List<User> usersNovos = await _userRepository.UpdateBatchUserAsync(usersAlterados);

        List<UserResponseAPI> usersResponsesAPI = new List<UserResponseAPI>();

        foreach (var user in usersNovos)
        {
            UserResponseAPI userResponse = new UserResponseAPI
            {
                Id = user.id_user,
                Name = user.name,
                Username = user.username,
                Email = user.email,
                Password = user.password,
                Ids_reviews = user.reviews.Select(r => r.id_review).ToList() ?? Enumerable.Empty<uint>().ToList()
            };

            usersResponsesAPI.Add(userResponse);
        }

        return usersResponsesAPI;
    }
    
    // DELETE
    public async Task<bool> DeleteUser(uint id)
    {
        return await _userRepository.DeleteUserAsync(id);
    }

}