using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UniMDB.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var Env_Var = builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly()).Build();

string connectionString = $"Server = {Environment.GetEnvironmentVariable("RAILWAY_TCP_PROXY_DOMAIN") ?? Env_Var["MYSQLHOST"]}; " +
                            $"Port = {Environment.GetEnvironmentVariable("RAILWAY_TCP_PROXY_PORT") ?? Env_Var["MYSQLPORT"]}; " +
                            $"Uid = {Environment.GetEnvironmentVariable("MYSQLUSER") ?? Env_Var["MYSQLUSER"]}; " +
                            $"Pwd = {Environment.GetEnvironmentVariable("MYSQLPASSWORD") ?? Env_Var["MYSQLPASSWORD"]}; " +
                            $"Database = {Environment.GetEnvironmentVariable("MYSQLDATABASE") ?? Env_Var["MYSQLDATABASE"]};";

Console.WriteLine(connectionString);

builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(
    connectionString,
    ServerVersion.AutoDetect(connectionString)
));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    var port = Environment.GetEnvironmentVariable("PORT") ?? "8081";
    builder.WebHost.UseUrls($"http://*:{port}");
}

app.UseSwagger();
app.UseSwaggerUI();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();