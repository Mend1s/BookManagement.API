using BookManagement.Core.Entities;
using BookManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure.Persistence.Repositories;

public class BookRepository : IBookReposiroty
{
    private readonly BooksManagementDbContext _dbContext;
    public BookRepository(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<Book>> GetAllAsync()
    {
        return await _dbContext.Books.ToListAsync();
    }
    public async Task<Book> GetByIdAsync(int id)
    {
        return await _dbContext.Books.SingleOrDefaultAsync(b => b.Id == id);
    }
    public async Task<Book> CreateAsync(Book book)
    {
        await _dbContext.Books.AddAsync(book);
        await _dbContext.SaveChangesAsync();
        return book;
    }
    public async Task UpdateAsync(Book book)
    {
        _dbContext.Books.Update(book);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(Book book)
    {
        _dbContext.Books.Remove(book);
        await _dbContext.SaveChangesAsync();
    }
}
