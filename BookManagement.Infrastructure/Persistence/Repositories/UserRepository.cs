using BookManagement.Core.Entities;
using BookManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private readonly BooksManagementDbContext _dbContext;
    public UserRepository(BooksManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _dbContext.Users.ToListAsync();
    }
    public async Task<User> GetByIdAsync(int id)
    {
        return await _dbContext.Users.SingleOrDefaultAsync(b => b.Id == id);
    }
    public async Task<User> CreateAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user;
    }
    public async Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
    public async Task DeleteAsync(User user)
    {
        _dbContext.Users.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
}
