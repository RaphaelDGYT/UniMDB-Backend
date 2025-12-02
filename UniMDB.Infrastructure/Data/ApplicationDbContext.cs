using Microsoft.EntityFrameworkCore;
using UniMDB.Domain.Entities;

namespace UniMDB.Infrastructure.Data;

//  Classe que representa o nosso Banco de Dados

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options) : base(options) {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }


    //  Cada propriedade dessa é uma tabela dentro do nossso banco de dados
    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Favorite> Favorites { get; set; }


}