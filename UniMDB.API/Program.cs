using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UniMDB.Application.Services;
using UniMDB.Domain.Interfaces;
using UniMDB.Domain.Entities;
using UniMDB.API.Utils;
using UniMDB.Infrastructure.Data;
using UniMDB.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;

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
    //  Obtem a string de conexão pelas variaveis de ambiente do Railway, caso não conseguir, usa 
    //  o 'user-secrets' para procurar a variável. Isso é útil especialmente para quando não estivermos em produção
    var Env_Var = builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly()).Build();

    string connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? Env_Var["ConnectionString"];

    if (connectionString == null)
    {
        throw new Exception("Erro na Connection String");
    }

    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), opt =>
    {
        opt.MigrationsAssembly("UniMDB.API");
    });
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8081";
    builder.WebHost.UseUrls($"http://*:{port}");
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    try
    {
        if (db.Database.CanConnect())
            Console.WriteLine("Conexão com o banco de dados OK!");
        else
            Console.WriteLine("Falha ao conectar ao banco de dados.");


        var userServ = new UserService(new UserRepository(db));
        var reviewServ = new ReviewService(new ReviewRepository(db));

        const uint qntd_users_adicionar = 0;
        const uint qntd_reviews_adicionar = 0;
        
        for (int i = 0; i < qntd_users_adicionar; i++)
        {
            await userServ.AddUser(GerarDados.GerarUser());
        }

        var lista_ids = await userServ.GetAllUserIds();

        for (int i = 0; i < qntd_reviews_adicionar; i++)
        {
            await reviewServ.AddReview(GerarDados.GerarReview(lista_ids));
        }

    }
    catch (Exception ex)
    {
        Console.WriteLine($"Erro: {ex.Message}");
    }
}

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();