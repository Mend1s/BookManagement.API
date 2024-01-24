using BookManagement.Application.ViewModels;
using BookManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Application.Queries.GetAllBooks
{
    public class GetAllBookQueryHandler : IRequestHandler<GetAllBookQuery, List<BookViewModel>>
    {
        private readonly BooksManagementDbContext _dbContext;
        public GetAllBookQueryHandler(BooksManagementDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<BookViewModel>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
        {
            var books =  _dbContext.Books;

            var booksViewModel = await books.Select(b => new BookViewModel(b.Id, b.Title, b.Author, b.Isbn, b.YearOfPublication)).ToListAsync();

            return booksViewModel;
        }
    }
}
