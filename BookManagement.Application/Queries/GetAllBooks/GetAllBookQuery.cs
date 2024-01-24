using BookManagement.Application.ViewModels;
using MediatR;

namespace BookManagement.Application.Queries.GetAllBooks;

public class GetAllBookQuery : IRequest<List<BookViewModel>>
{
}
