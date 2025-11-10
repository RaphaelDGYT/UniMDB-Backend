using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UniMDB.Application.Services;
using UniMDB.Infrastructure.Data;
using UniMDB.Domain.Interfaces; 
using UniMDB.Infrastructure.Repositories; 


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IReviewService, ReviewService>();





// Conex�o ao Banco de Dados
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    //  Obtem a string de conex�o pelas variaveis de ambiente do Railway, caso n�o conseguir, usa 
    //  o 'user-secrets' para procurar a vari�vel. Isso � �til especialmente para quando n�o estivermos em produ��o
    var Env_Var = builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly()).Build();

    string? connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") ?? Env_Var["ConnectionString"]
                                                                    ?? "Server=shinkansen.proxy.rlwy.net;Port=50334;Database=railway;User=root;Password=XbJaZGCKLonxNxnybAPlZnfvdfOaktia;";

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

//  Configura��es feitas para quando estivermos em produ��o apenas
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