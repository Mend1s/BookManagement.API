using BookManagement.Core.Entities;
using BookManagement.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure.Persistence.Repositories
{
    public class LoanRepository : ILoanRepository
    {
        private readonly BooksManagementDbContext _dbContext;
        public LoanRepository(BooksManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<Loan>> GetAllAsync()
        {
            return await _dbContext.Loans.ToListAsync();
        }
        public async Task<Loan> GetByIdAsync(int id)
        {
            return await _dbContext.Loans.SingleOrDefaultAsync(b => b.Id == id);
        }
        public async Task<Loan> CreateAsync(Loan loan)
        {
            await _dbContext.Loans.AddAsync(loan);
            await _dbContext.SaveChangesAsync();
            return loan;
        }
        public async Task UpdateAsync(Loan loan)
        {
            _dbContext.Loans.Update(loan);
            await _dbContext.SaveChangesAsync();
        }
        public async Task DeleteAsync(Loan loan)
        {
            _dbContext.Loans.Remove(loan);
            await _dbContext.SaveChangesAsync();
        }
    }
}
