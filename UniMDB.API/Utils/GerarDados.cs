using Bogus;
using System.ComponentModel.DataAnnotations;
using UniMDB.Application.Dtos;
using UniMDB.Domain.Entities;

namespace UniMDB.API.Utils;

public static class GerarDados
{
    public static UserCreation GerarUser()
    {
        var faker = new Faker<UserCreation>("pt_BR")
            .RuleFor(u => u.Name, f => f.Person.FullName)
            .RuleFor(u => u.Username, f => f.Internet.UserName(f.Person.FirstName, f.Person.LastName))
            .RuleFor(u => u.Email, f => f.Person.Email)
            .RuleFor(u => u.Password, f => f.Internet.Password());

        return faker.Generate();
    }

    public static ReviewCreation GerarReview(List<uint> lista_ids)
    {
        Random rand = new Random();
        var faker = new Faker<ReviewCreation>("pt_BR")
                .RuleFor(r => r.Id_User, f => f.PickRandom(lista_ids))
                .RuleFor(r => r.Id_Movie, f => $"tt{f.Random.UInt(max: 9_999_999):0000000}")
                .RuleFor(r => r.Score, f => f.Random.Byte(max: 10))
                .RuleFor(r => r.Comment, f => f.Rant.Review("movie"));

        return faker.Generate();
    }
}
