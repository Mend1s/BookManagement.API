using BookManagement.Application.ViewModels;
using MediatR;

namespace BookManagement.Application.Queries.GetBookById;

public class GetBookByIdQuery : IRequest<BookViewModel>
{
    public GetBookByIdQuery(int id)
    {
        Id = id;
    }

    public int Id { get; set; }
}
