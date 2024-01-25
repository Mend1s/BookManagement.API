using BookManagement.Application.ViewModels;
using BookManagement.Core.Repositories;
using MediatR;

namespace BookManagement.Application.Queries.GetAllBooks
{
    public class GetAllBookQueryHandler : IRequestHandler<GetAllBookQuery, List<BookViewModel>>
    {
        private readonly IBookReposiroty _bookReposiroty;
        public GetAllBookQueryHandler(IBookReposiroty bookReposiroty)
        {
            _bookReposiroty = bookReposiroty;
        }
        public async Task<List<BookViewModel>> Handle(GetAllBookQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookReposiroty.GetAllAsync();

            var booksViewModel = books
                .Select(b => new BookViewModel(b.Id, b.Title, b.Author, b.Isbn, b.YearOfPublication))
                .ToList();

            return booksViewModel;
        }
    }
}
