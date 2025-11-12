using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UniMDB.API.Utils;
using UniMDB.Application.Services;
using UniMDB.Domain.Interfaces;
using UniMDB.Domain.Entities;
using UniMDB.Infrastructure.Data;
using UniMDB.Infrastructure.Repositories;

//const int numero_usuarios_fakes = 10;
//Random rand = new Random();

//User[] usuarios_fakes = new User[numero_usuarios_fakes];

//for (int i = 0; i < numero_usuarios_fakes; i++)
//{
//    usuarios_fakes[i] = GeradorDados.GerarUsuario();
//}

//Review[] reviews_fakes = new Review[rand.Next(5, 15)];

//for (int i = 0; i < reviews_fakes.Length; i++)
//{
//    reviews_fakes[i] = new Review()
//    {
//        id_review_user = (uint)rand.Next(numero_usuarios_fakes),
//        id_review = 0,
//        created_at = DateTime.Now,
//        id_movie_mdb = $"ffff{i}",
//        comment = Faker.Lorem.Sentence()
//    };
//}

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Conexão ao Banco de Dados
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

//  Configurações feitas para quando estivermos em produção apenas
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