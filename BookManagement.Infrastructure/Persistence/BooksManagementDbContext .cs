using BookManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure.Persistence;

public class BooksManagementDbContext : DbContext
{
    public BooksManagementDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Book> Books { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Loan> Loans { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BooksManagementDbContext).Assembly);
    }
}
