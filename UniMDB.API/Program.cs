using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UniMDB.Application.Services;
using UniMDB.Domain.Interfaces;
using UniMDB.Domain.Entities;
using UniMDB.Infrastructure.Data;
using UniMDB.Infrastructure.Repositories;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography.X509Certificates;
using UniMDB.Application.Dtos;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IFavoriteRepository, FavoriteRepository>();
builder.Services.AddScoped<IFavoriteService, FavoriteService>();






// 
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])
            )
        };
    });

builder.Services.AddScoped<JwtService>();

//o trecho acaba aqui tudo entre esse comentario pode apagar isso é utilizado apenas para teste via codespace

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



app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseDefaultFiles();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.MapGet("/", () => Results.Redirect("/index.html"));

app.Run();