namespace UniMDB.Application.Services;

public interface IJwtService
{
    string GenerateToken(uint id, string email, string username);
}

