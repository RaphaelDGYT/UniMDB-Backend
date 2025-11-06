using Microsoft.EntityFrameworkCore;
using UniMDB.Domain.Entities;

namespace UniMDB.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Review> Reviews { get; set; }
}