using BookManagement.Application.Commands.CreateBook;
using BookManagement.Application.Commands.DeleteBook;
using BookManagement.Application.Commands.UpdateBook;
using BookManagement.Application.Queries.GetAllBooks;
using BookManagement.Application.ViewModels;
using BookManagement.Infrastructure.Persistence;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.API.Controllers;

[Route("api/[controller]")]
public class BooksController : ControllerBase
{
    private readonly BooksManagementDbContext _dbContext;
    private readonly IMediator _mediator;
    public BooksController(BooksManagementDbContext dbContext, IMediator mediator)
    {
        _dbContext = dbContext;
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<BookViewModel>>> GetAll()
    {
        var query = new GetAllBookQuery();

        var books = await _mediator.Send(query);

        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BookViewModel>> GetById(int id)
    {
        var book = await _dbContext.Books.SingleOrDefaultAsync(b => b.Id == id);

        if (book is null) return NotFound();

        var bookViewModel = new BookViewModel
        {
            Id = book.Id,
            Title = book.Title,
            Author = book.Author,
            ISBN = book.ISBN,
            YearOfPublication = book.YearOfPublication
        };

        return bookViewModel;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateBookCommand command)
    {
        if (command is null) return BadRequest();

        var id = await _mediator.Send(command);

        return CreatedAtAction(nameof(GetById), new { id = id }, command);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] UpdateBookCommand updateBookCommand)
    {
        var command = new UpdateBookCommand
            (id,
            updateBookCommand.Title,
            updateBookCommand.Author,
            updateBookCommand.ISBN,
            updateBookCommand.YearOfPublication);

        await _mediator.Send(command);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var command = new DeleteBookCommand(id);

        await _mediator.Send(command);

        return NoContent();
    }
}
