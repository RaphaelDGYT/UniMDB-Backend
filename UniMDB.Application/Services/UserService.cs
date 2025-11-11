using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;
using UniMDB.Infrastructure.Data;

namespace UniMDB.Application.Services;

//  Aqui vai ficar todas as implementações que envolvam o banco de dados e os usuários

//  TODO: Fazer as implementações   xD

public class UserService : IUserService
{
    private readonly ApplicationDbContext _appDbContext;
    public UserService(ApplicationDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public Task<bool> DeleteUser(uint id)
    {
        throw new NotImplementedException();
    }

    public Task<List<UserResponseAPI>> GetAllUsers()
    {
        throw new NotImplementedException();
    }

    public Task<UserResponseAPI> GetUser(uint id)
    {
        throw new NotImplementedException();
    }

    public Task<UserResponseAPI> AddUser(UserRegistration user)
    {
        throw new NotImplementedException();
    }

    public Task<UserResponseAPI> UpdateUser(uint id, UserRegistration user)
    {
        throw new NotImplementedException();
    }

    Task<User> IUserService.GetUserByReview(uint id_review)
    {
        throw new NotImplementedException();
    }
}