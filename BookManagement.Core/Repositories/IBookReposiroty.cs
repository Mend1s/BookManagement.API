using BookManagement.Core.Entities;

namespace BookManagement.Core.Repositories
{
    public interface IBookReposiroty
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);
        Task<Book> CreateAsync(Book book);
        Task UpdateAsync(Book book);
        Task DeleteAsync(Book book);
    }
}
