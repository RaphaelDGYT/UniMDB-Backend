using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;
using UniMDB.Domain.Interfaces;

namespace UniMDB.Application.Services;

public class UserService : IUserService
{
    // a partir daqui será feita a distribuição do service para o repository
    // utilizará o gerador do token.
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;
    
    public UserService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
    }


    // CREATE
    public async Task<UserResponse> AddUser(UserCreation user)
    {
        try
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

            return new UserResponse
            {
                Id = userNovo.id_user,
                Name = user.Name,
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
                Ids_reviews = Enumerable.Empty<uint>().ToList()
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<List<UserResponse>> AddBatchUser(List<UserCreation> users)
    {
        try
        {
            List<User> batchUsers = new List<User>(users.Count);

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

            // Resposta

            List<UserResponse> usersResponses = new List<UserResponse>();

            foreach (var user in usersAdicionados)
            {
                usersResponses.Add(new UserResponse
                {
                    Id = user.id_user,
                    Name = user.name,
                    Username = user.username,
                    Email = user.email,
                    Password = user.password,
                    Ids_reviews = Enumerable.Empty<uint>().ToList()
                });
            }

            return usersResponses;
        }
        catch (Exception)
        {
            throw;
        }
    }

    // READ
    public async Task<List<uint>> GetAllUserIds()
    {
        return await _userRepository.GetAllUserIdsAsync();
    }
    
    public async Task<UserResponse> GetUserById(uint id)
    {
        try
        {
            User? user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return new UserResponse();
            }

            List<Review> reviews = await _userRepository.GetAllReviewsByUserIdAsync(id);

            return new UserResponse 
            {
                Id = id,
                Name = user.name,
                Username = user.username,
                Email = user.email,
                Password = user.password,
                Ids_reviews = reviews
                                .Select(u => u.id_review)
                                .ToList() 
                                ?? 
                                Enumerable.Empty<uint>().ToList()
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    public async Task<LoginResponse> GetUserBySession(UserLogin user)
    {
        try
        {
            // Busca o usuário no banco
            User userProcurado = await _userRepository.GetUserBySessionAsync(new User
            {
                email = user.Email,
                password = user.Password
            });

            // Se não encontrou
            if (userProcurado == null)
            {
                return null; 
            }

            // Gera o token JWT
            string token = _jwtService.GenerateToken(
                userProcurado.id_user,
                userProcurado.email,
                userProcurado.username
            );

            // Retorna os dados + token
            return new LoginResponse
            {
                Id = userProcurado.id_user,
                Name = userProcurado.name,
                Username = userProcurado.username,
                Email = userProcurado.email,
                Token = token
            };
        }
            catch (Exception)
            {
                throw;
            }
    }

    public async Task<UserReviewsResponse> GetAllReviewsByUserId(uint id)
    {
        try
        {
            User user = await _userRepository.GetUserByIdAsync(id);

            if (user == null)
            {
                return new UserReviewsResponse
                {
                    UserAutor = new User
                    {
                        id_user = 0,
                        name = string.Empty,
                        username = string.Empty,
                        email = string.Empty,
                        password = string.Empty
                    },
                    Reviews = Enumerable.Empty<ReviewResponseList>().ToList()
                };
            }

            List<Review> reviews = await _userRepository.GetAllReviewsByUserIdAsync(id);

            if (reviews.Count == 0)
            {
                return new UserReviewsResponse
                {
                    UserAutor = user,
                    Reviews = Enumerable.Empty<ReviewResponseList>().ToList()
                };
            }

            // Resposta

            List<ReviewResponseList> reviewsResponses = new List<ReviewResponseList>(reviews.Count);

            foreach (var review in reviews)
            {
                reviewsResponses.Add(new ReviewResponseList
                {
                    Id = review.id_review,
                    Movie_Id = review.id_movie_mdb,
                    Score = review.review,
                    Comment = review.comment,
                    Created_at = review.created_at
                });
            }

            return new UserReviewsResponse
            {
                UserAutor = user,
                Reviews = reviewsResponses
            };
        }
        catch (Exception)
        {
            throw;
        }
    }

    // UPDATE
    public async Task<UserResponse> UpdateUser(uint id, UserCreation user)
    {
        try
        {
            User? userExistente = await _userRepository.GetUserByIdAsync(id);

            if (userExistente == null)
            {
                return new UserResponse();
            }

            User userAlterado = new User
            {
                id_user = id,
                name = string.IsNullOrEmpty(user.Name.Trim()) ? userExistente.name : user.Name,
                username = string.IsNullOrEmpty(user.Username.Trim()) ? userExistente.username : user.Username,
                email = string.IsNullOrEmpty(user.Email.Trim()) ? userExistente.email : user.Email,
                password = string.IsNullOrEmpty(user.Password.Trim()) ? userExistente.password : user.Password
            };

            User? userNovo = await _userRepository.UpdateUserAsync(id, userAlterado);

            if (userNovo == null)
            {
                return new UserResponse();
            }

            List<Review> reviews = await _userRepository.GetAllReviewsByUserIdAsync(id);

            return new UserResponse
            {
                Id = userNovo.id_user,
                Name = userNovo.name,
                Username = userNovo.username,
                Email = userNovo.email,
                Password = userNovo.password,
                Ids_reviews = reviews
                                .Select(u => u.id_review)
                                .ToList()
                                ??
                                Enumerable.Empty<uint>().ToList()
            };
        }
        catch (Exception)
        {
            throw;
        }
    }
    public async Task<List<UserResponse>> UpdateBatchUser(List<(uint, UserCreation)> users)
    {
        try
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

            List<UserResponse> usersResponses = new List<UserResponse>();

            foreach (var user in usersNovos)
            {
                UserResponse userResponse = new UserResponse
                {
                    Id = user.id_user,
                    Name = user.name,
                    Username = user.username,
                    Email = user.email,
                    Password = user.password,
                    Ids_reviews = user.reviews
                                        .Select(r => r.id_review)
                                        .ToList() 
                                        ?? 
                                        Enumerable.Empty<uint>().ToList()
                };

                usersResponses.Add(userResponse);
            }

            return usersResponses;
        }
        catch (Exception)
        {
            throw;
        }

    }
    
    // DELETE
    public async Task<bool> DeleteUser(uint id)
    {
        return await _userRepository.DeleteUserAsync(id);
    }

}