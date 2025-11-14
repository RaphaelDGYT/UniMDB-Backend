using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UniMDB.Application.Services;
using UniMDB.Domain.Interfaces;
using UniMDB.Domain.Entities;
using UniMDB.API.Utils;
using UniMDB.Infrastructure.Data;
using UniMDB.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.X509Certificates;
using UniMDB.Application.Dtos;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    var Env_Var = builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly()).Build();

    string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? Env_Var["ConnectionString"];

    if (connectionString == null)
    {
        throw new Exception("Erro na Connection String");
    }

    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), opt =>
        opt.MigrationsAssembly("UniMDB.API")
    );
});

var app = builder.Build();

using (var scope = app.Services.CreateAsyncScope()) 
{

    try
    {
        var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (!db.Database.CanConnect())
            throw new Exception("\n\nErro: N�o foi poss�vel conectar no Banco de Dados\n\n");
    }
    catch (Exception)
    {
        throw;
    }
}

if (!app.Environment.IsDevelopment())
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8081";
    builder.WebHost.UseUrls($"http://*:{port}");
}
else
{
    using (var scope = app.Services.CreateAsyncScope())
    {
        var db = scope.ServiceProvider.GetService<ApplicationDbContext>();
        UserService userService = new UserService(new UserRepository(db));
        ReviewService reviewService = new ReviewService(new ReviewRepository(db));
        
        const uint qntd_users_fakes = 0;
        const uint qntd_reviews_fakes = 0;

        if (qntd_users_fakes > 0)
        {
            List<UserCreation> usuarios = new List<UserCreation>();

            for (int i = 0; i < qntd_users_fakes; i++)
            {
                usuarios.Add(GerarDados.GerarUser());
            }

            await userService.AddBatchUser(usuarios);
        }

        if (qntd_reviews_fakes > 0)
        {
            List<uint> ids_usuarios = await userService.GetAllUserIds();
            List<ReviewCreation> reviews = new List<ReviewCreation>();

            for (int i = 0; i < qntd_reviews_fakes; i++)
            {
                reviews.Add(GerarDados.GerarReview(ids_usuarios));
            }

            await reviewService.AddBatchReview(reviews);
        }

    }
}



app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();